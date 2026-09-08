-- Migration 1: Add columns and tables for production features
ALTER TABLE users ADD COLUMN IF NOT EXISTS MustChangePassword BOOLEAN DEFAULT FALSE;
ALTER TABLE sales ADD COLUMN IF NOT EXISTS payment_method VARCHAR(50);
ALTER TABLE sales ADD COLUMN IF NOT EXISTS cashier_id INTEGER REFERENCES users(Id);
ALTER TABLE stock_movements ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);
ALTER TABLE returns ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);
ALTER TABLE z_reports ADD COLUMN IF NOT EXISTS branch_id INTEGER REFERENCES branches(Id);

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

-- Ensure admin must change default password
UPDATE users SET MustChangePassword = TRUE WHERE Username = 'Admin' AND MustChangePassword IS NOT TRUE;

INSERT INTO schema_migrations (version) SELECT 1 WHERE NOT EXISTS (SELECT 1 FROM schema_migrations WHERE version = 1);
