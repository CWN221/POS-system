
-- Users Table
CREATE TABLE IF NOT EXISTS users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Role VARCHAR(50) NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    MustChangePassword BOOLEAN DEFAULT FALSE,
    BranchId INTEGER NOT NULL,
    FailedLoginAttempts INTEGER DEFAULT 0,
    LockoutUntil TIMESTAMP,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Branches Table
CREATE TABLE IF NOT EXISTS branches (
    Id SERIAL PRIMARY KEY,
    BranchName VARCHAR(100) NOT NULL,
    Location TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Store Settings Table
CREATE TABLE IF NOT EXISTS StoreSettings (
    Id SERIAL PRIMARY KEY,
    BranchId INTEGER REFERENCES branches(Id),
    StoreName VARCHAR(200),
    Address TEXT,
    Phone1 VARCHAR(20),
    Phone2 VARCHAR(20),
    Email VARCHAR(100),
    Business_PIN VARCHAR(50),
    LogoPath TEXT
);

-- Products Table
CREATE TABLE IF NOT EXISTS products (
    idproducts SERIAL PRIMARY KEY,
    pName VARCHAR(200) NOT NULL,
    sku VARCHAR(100),
    pCategory VARCHAR(100),
    price DECIMAL(10,2),
    sellingPrice DECIMAL(10,2),
    vatCategory VARCHAR(50),
    stock_status VARCHAR(20),
    stock_quantity INTEGER DEFAULT 0,
    low_stock_threshold INTEGER DEFAULT 5,
    is_active BOOLEAN DEFAULT TRUE,
    BranchId INTEGER REFERENCES branches(Id)
);

-- Product Categories Table
CREATE TABLE IF NOT EXISTS product_categories (
    Id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);

-- VAT Settings Table
CREATE TABLE IF NOT EXISTS vat_settings (
    Id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    vat_percentage DECIMAL(5,2) NOT NULL
);

CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Sales Table
CREATE TABLE IF NOT EXISTS sales (
    id SERIAL PRIMARY KEY,
    sale_uuid UUID DEFAULT gen_random_uuid(),
    sale_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(10,2),
    BranchId INTEGER REFERENCES branches(Id),
    payment_method VARCHAR(50),
    receipt_number VARCHAR(50) NOT NULL,
    cashier_id INTEGER REFERENCES users(Id),
    store_credit_used NUMERIC(14,2) DEFAULT 0
);

-- Sale Items Table
CREATE TABLE IF NOT EXISTS sale_items (
    id SERIAL PRIMARY KEY,
    sale_id INTEGER REFERENCES sales(id),
    product_id INTEGER REFERENCES products(idproducts),
    product_name VARCHAR(200),
    unit_price DECIMAL(10,2),
    selling_price DECIMAL(10,2),
    profit DECIMAL(10,2),
    discount DECIMAL(10,2),
    quantity INTEGER,
    vat_percentage DECIMAL(5,2),
    vat_amount DECIMAL(10,2),
    vat_category VARCHAR(50),
    total_price DECIMAL(10,2)
);

-- Stock Movements Table
CREATE TABLE IF NOT EXISTS stock_movements (
    id SERIAL PRIMARY KEY,
    product_id INTEGER REFERENCES products(idproducts),
    type VARCHAR(20),
    quantity INTEGER,
    reference_id INTEGER,
    branch_id INTEGER REFERENCES branches(Id),
    reason VARCHAR(200),
    user_id INTEGER REFERENCES users(Id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Returns Table
CREATE TABLE IF NOT EXISTS returns (
    id SERIAL PRIMARY KEY,
    receipt_no VARCHAR(100),
    sale_id INTEGER REFERENCES sales(id),
    reason TEXT,
    refund_type VARCHAR(50),
    refund_amount DECIMAL(10,2),
    requested_by INTEGER REFERENCES users(Id),
    approved_by INTEGER REFERENCES users(Id),
    branch_id INTEGER REFERENCES branches(Id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Return Items Table
CREATE TABLE IF NOT EXISTS return_items (
    id SERIAL PRIMARY KEY,
    return_id INTEGER REFERENCES returns(id),
    sale_item_id INTEGER REFERENCES sale_items(id),
    product_id INTEGER REFERENCES products(idproducts),
    qty_returned INTEGER,
    line_amount DECIMAL(10,2),
    customer_phone VARCHAR(30)
);

-- Z Reports Table
CREATE TABLE IF NOT EXISTS z_reports (
    id SERIAL PRIMARY KEY,
    report_date DATE NOT NULL,
    branch_id INTEGER REFERENCES branches(Id),
    total_sales DECIMAL(10,2),
    total_vat DECIMAL(10,2),
    total_transactions INTEGER,
    gross_sales DECIMAL(10,2),
    refund_amount DECIMAL(10,2),
    net_sales DECIMAL(10,2),
    cash_total DECIMAL(10,2),
    mpesa_total DECIMAL(10,2),
    card_total DECIMAL(10,2),
    closed_by INTEGER REFERENCES users(Id),
    is_locked BOOLEAN DEFAULT FALSE,
    generated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(report_date, branch_id)
);

-- Audit Log Table
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

-- Payments Table
CREATE TABLE IF NOT EXISTS payments (
    id SERIAL PRIMARY KEY,
    sale_id INTEGER NOT NULL REFERENCES sales(id) ON DELETE CASCADE,
    method VARCHAR(30) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    reference VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Suppliers Table
CREATE TABLE IF NOT EXISTS suppliers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    contact_person VARCHAR(150),
    phone VARCHAR(30),
    email VARCHAR(150),
    address TEXT,
    kra_pin VARCHAR(50),
    is_active BOOLEAN DEFAULT TRUE,
    branch_id INTEGER REFERENCES branches(Id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Purchase Orders Table
CREATE TABLE IF NOT EXISTS purchase_orders (
    id SERIAL PRIMARY KEY,
    po_number VARCHAR(50) NOT NULL,
    supplier_id INTEGER NOT NULL REFERENCES suppliers(id),
    branch_id INTEGER NOT NULL REFERENCES branches(Id),
    status VARCHAR(20) NOT NULL DEFAULT 'DRAFT',
    order_date DATE NOT NULL DEFAULT CURRENT_DATE,
    expected_date DATE,
    notes TEXT,
    created_by INTEGER REFERENCES users(Id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Purchase Order Items Table
CREATE TABLE IF NOT EXISTS purchase_order_items (
    id SERIAL PRIMARY KEY,
    po_id INTEGER NOT NULL REFERENCES purchase_orders(id) ON DELETE CASCADE,
    product_id INTEGER NOT NULL REFERENCES products(idproducts),
    quantity_ordered INTEGER NOT NULL,
    quantity_received INTEGER NOT NULL DEFAULT 0,
    unit_cost DECIMAL(10,2) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Goods Receipt Table
CREATE TABLE IF NOT EXISTS goods_receipts (
    id SERIAL PRIMARY KEY,
    receipt_number VARCHAR(50) NOT NULL,
    po_id INTEGER NOT NULL REFERENCES purchase_orders(id),
    branch_id INTEGER NOT NULL REFERENCES branches(Id),
    received_by INTEGER REFERENCES users(Id),
    notes TEXT,
    received_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Goods Receipt Items Table
CREATE TABLE IF NOT EXISTS goods_receipt_items (
    id SERIAL PRIMARY KEY,
    receipt_id INTEGER NOT NULL REFERENCES goods_receipts(id) ON DELETE CASCADE,
    po_item_id INTEGER NOT NULL REFERENCES purchase_order_items(id),
    product_id INTEGER NOT NULL REFERENCES products(idproducts),
    quantity_received INTEGER NOT NULL,
    unit_cost DECIMAL(10,2) NOT NULL
);

-- Store Credit Ledger Table
CREATE TABLE IF NOT EXISTS store_credit_ledger (
    id SERIAL PRIMARY KEY,
    customer_phone VARCHAR(30) NOT NULL,
    amount NUMERIC(14,2) NOT NULL,          -- positive = credit issued, negative = redeemed
    movement_type VARCHAR(20) NOT NULL,     -- 'ISSUE' or 'REDEEM'
    reference_id INTEGER,                   -- return_id for ISSUE, sale_id for REDEEM
    branch_id INTEGER REFERENCES branches(Id),
    user_id INTEGER REFERENCES users(Id),
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS sync_log (
    id              BIGSERIAL PRIMARY KEY,
    table_name      VARCHAR(50) NOT NULL,
    local_row_id    INTEGER NOT NULL,
    synced_at       TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT uq_sync_log UNIQUE (table_name, local_row_id)
);

-- Track the sequence explicitly for future inserts
CREATE TABLE IF NOT EXISTS receipt_sequences (
    year INT PRIMARY KEY,
    last_sequence INT NOT NULL DEFAULT 0
);

-- Schema version tracking
CREATE TABLE IF NOT EXISTS schema_migrations (
    version INTEGER PRIMARY KEY,
    applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Indexes for Performance
CREATE INDEX IF NOT EXISTS idx_sales_branch ON sales(BranchId);
CREATE INDEX IF NOT EXISTS idx_sales_date ON sales(sale_date);
CREATE INDEX IF NOT EXISTS idx_sale_items_sale ON sale_items(sale_id);
CREATE INDEX IF NOT EXISTS idx_products_branch ON products(BranchId);
CREATE INDEX IF NOT EXISTS idx_stock_movements_product ON stock_movements(product_id);
CREATE INDEX IF NOT EXISTS idx_returns_sale ON returns(sale_id);
CREATE INDEX IF NOT EXISTS idx_audit_log_created ON audit_log(created_at);
CREATE INDEX IF NOT EXISTS idx_return_items_sale_item_id ON return_items(sale_item_id);
CREATE INDEX IF NOT EXISTS idx_payments_sale_id ON payments(sale_id);
CREATE INDEX IF NOT EXISTS idx_stock_movements_created ON stock_movements(created_at);
CREATE INDEX IF NOT EXISTS idx_stock_movements_branch_type ON stock_movements(branch_id, type);
CREATE INDEX IF NOT EXISTS idx_users_lockout ON users(LockoutUntil);
CREATE UNIQUE INDEX IF NOT EXISTS idx_sales_uuid ON sales(sale_uuid);
CREATE INDEX IF NOT EXISTS idx_suppliers_branch ON suppliers(branch_id);
CREATE INDEX IF NOT EXISTS idx_suppliers_active ON suppliers(is_active);
CREATE INDEX IF NOT EXISTS idx_po_branch ON purchase_orders(branch_id);
CREATE INDEX IF NOT EXISTS idx_po_supplier ON purchase_orders(supplier_id);
CREATE INDEX IF NOT EXISTS idx_po_status ON purchase_orders(status);
CREATE UNIQUE INDEX IF NOT EXISTS idx_po_number ON purchase_orders(po_number);
CREATE INDEX IF NOT EXISTS idx_po_items_po ON purchase_order_items(po_id);
CREATE INDEX IF NOT EXISTS idx_po_items_product ON purchase_order_items(product_id);
CREATE INDEX IF NOT EXISTS idx_gr_po ON goods_receipts(po_id);
CREATE INDEX IF NOT EXISTS idx_gr_branch ON goods_receipts(branch_id);
CREATE INDEX IF NOT EXISTS idx_gr_items_receipt ON goods_receipt_items(receipt_id);
CREATE UNIQUE INDEX IF NOT EXISTS idx_sales_receipt_number ON sales(receipt_number);
CREATE INDEX IF NOT EXISTS idx_products_active ON products(BranchId, is_active);
CREATE INDEX IF NOT EXISTS idx_credit_phone ON store_credit_ledger(customer_phone);
CREATE INDEX IF NOT EXISTS idx_credit_branch ON store_credit_ledger(branch_id, customer_phone);
CREATE INDEX IF NOT EXISTS idx_returns_phone ON returns(customer_phone);
CREATE INDEX IF NOT EXISTS idx_sync_log_table ON sync_log(table_name);


-- Insert default VAT settings
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Standard', 16.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Standard');
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Zero Rated', 0.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Zero Rated');
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Exempt', 0.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Exempt');


-- Insert default admin user (password: Admin123!) - must change on first login
INSERT INTO users (Username, PasswordHash, Role, IsActive, MustChangePassword, BranchId)
SELECT 'Admin', '$2a$12$lAtVrfdV8/UBM4NlS99VD.Vn0s4.4LSAbECNNn0POO39kSBDkM2h2', 'Admin', TRUE, TRUE, 1
WHERE NOT EXISTS (SELECT 1 FROM users WHERE Username = 'Admin');
