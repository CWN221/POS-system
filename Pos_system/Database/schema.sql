
-- Users Table
CREATE TABLE IF NOT EXISTS users (
    Id SERIAL PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    Role VARCHAR(50) NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    MustChangePassword BOOLEAN DEFAULT FALSE,
    BranchId INTEGER NOT NULL,
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

-- Sales Table
CREATE TABLE IF NOT EXISTS sales (
    id SERIAL PRIMARY KEY,
    sale_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(10,2),
    BranchId INTEGER REFERENCES branches(Id),
    payment_method VARCHAR(50),
    cashier_id INTEGER REFERENCES users(Id)
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
    line_amount DECIMAL(10,2)
);

-- Z Reports Table
CREATE TABLE IF NOT EXISTS z_reports (
    id SERIAL PRIMARY KEY,
    report_date DATE NOT NULL,
    branch_id INTEGER REFERENCES branches(Id),
    total_sales DECIMAL(10,2),
    total_vat DECIMAL(10,2),
    total_transactions INTEGER,
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

-- Insert default VAT settings
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Standard', 16.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Standard');
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Zero Rated', 0.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Zero Rated');
INSERT INTO vat_settings (name, vat_percentage)
SELECT 'Exempt', 0.00 WHERE NOT EXISTS (SELECT 1 FROM vat_settings WHERE name = 'Exempt');

-- Insert default branch
INSERT INTO branches (BranchName, Location)
SELECT 'Main Branch', 'City' WHERE NOT EXISTS (SELECT 1 FROM branches WHERE BranchName = 'Main Branch');

-- Insert default admin user (password: Admin123!) - must change on first login
INSERT INTO users (Username, PasswordHash, Role, IsActive, MustChangePassword, BranchId)
SELECT 'Admin', '$2a$12$lAtVrfdV8/UBM4NlS99VD.Vn0s4.4LSAbECNNn0POO39kSBDkM2h2', 'Admin', TRUE, TRUE, 1
WHERE NOT EXISTS (SELECT 1 FROM users WHERE Username = 'Admin');
