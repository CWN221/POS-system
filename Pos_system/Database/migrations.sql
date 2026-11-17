-- Migration 1: Add columns and tables for production features
ALTER TABLE users ADD COLUMN IF NOT EXISTS MustChangePassword BOOLEAN DEFAULT FALSE;
ALTER TABLE sales ADD COLUMN IF NOT EXISTS payment_method VARCHAR(50);
ALTER TABLE sales ADD COLUMN IF NOT EXISTS cashier_id INTEGER REFERENCES users(Id);
ALTER TABLE stock_movements ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);
ALTER TABLE returns ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);
ALTER TABLE z_reports ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);
ALTER TABLE sales ADD COLUMN IF NOT EXISTS amount_tendered DECIMAL(10,2), ADD COLUMN IF NOT EXISTS change_given DECIMAL(10,2);
ALTER TABLE sales
    ADD COLUMN IF NOT EXISTS is_voided BOOLEAN DEFAULT FALSE,
    ADD COLUMN IF NOT EXISTS voided_by INTEGER REFERENCES users(Id),
    ADD COLUMN IF NOT EXISTS voided_at TIMESTAMP,
    ADD COLUMN IF NOT EXISTS void_reason TEXT;

CREATE TABLE IF NOT EXISTS audit_log (
    id SERIAL PRIMARY KEY,
    user_id INTEGER REFERENCES users(Id),
    branch_id INTEGER REFERENCES branches(Id),
    action VARCHAR(100) NOT NULL,
    entity_type VARCHAR(50),
    entity_id VARCHAR(50),
    details TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS schema_migrations (
    version INTEGER PRIMARY KEY,
    applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX IF NOT EXISTS idx_z_reports_date_branch ON z_reports(report_date, branch_id);
CREATE INDEX IF NOT EXISTS idx_audit_log_created ON audit_log(created_at);
CREATE INDEX IF NOT EXISTS idx_sales_voided ON sales(is_voided);

-- Ensure admin must change default password
UPDATE users SET MustChangePassword = TRUE WHERE Username = 'Admin' AND MustChangePassword IS NOT TRUE;

INSERT INTO schema_migrations (version) SELECT 1 WHERE NOT EXISTS (SELECT 1 FROM schema_migrations WHERE version = 1);


-- Migrataion 2: Add M-Pesa integration columns and tables
ALTER TABLE sales ADD COLUMN IF NOT EXISTS mpesa_status VARCHAR(20) DEFAULT 'None';
ALTER TABLE payments ADD COLUMN IF NOT EXISTS mpesa_checkout_request_id VARCHAR(100);
ALTER TABLE payments ADD COLUMN IF NOT EXISTS mpesa_result_code VARCHAR(20);
ALTER TABLE payments ADD COLUMN IF NOT EXISTS mpesa_result_desc TEXT;

CREATE UNIQUE INDEX IF NOT EXISTS idx_payments_mpesa_checkout
    ON payments(mpesa_checkout_request_id)
    WHERE mpesa_checkout_request_id IS NOT NULL;

CREATE TABLE IF NOT EXISTS mpesa_stk_push_log (
    id BIGSERIAL PRIMARY KEY,
    sale_id INTEGER REFERENCES sales(id),
    checkout_request_id VARCHAR(100) UNIQUE,
    merchant_request_id VARCHAR(100),
    phone VARCHAR(20) NOT NULL,
    amount NUMERIC(14,2) NOT NULL,
    request_json JSONB,
    response_json JSONB,
    callback_json JSONB,
    status VARCHAR(20) NOT NULL DEFAULT 'Initiated',
    result_code VARCHAR(20),
    result_desc TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_mpesa_log_status ON mpesa_stk_push_log(status);
CREATE INDEX IF NOT EXISTS idx_mpesa_log_sale ON mpesa_stk_push_log(sale_id);

INSERT INTO schema_migrations (version) SELECT 2 WHERE NOT EXISTS (SELECT 1 FROM schema_migrations WHERE version = 2);