-- 1. Crear la Base de Datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BumburysRestaurantDB')
BEGIN
    CREATE DATABASE BumburysRestaurantDB;
END
GO

USE BumburysRestaurantDB;
GO

-- 2. Crear Tablas de Catálogo
CREATE TABLE roles (
    id_rol SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre_rol VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(150) NULL
);

CREATE TABLE usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    id_rol SMALLINT NOT NULL,
    nombre_completo VARCHAR(100) NOT NULL,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_usuarios_roles FOREIGN KEY (id_rol) REFERENCES roles(id_rol)
);

CREATE TABLE categorias (
    id_categoria SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre_categoria VARCHAR(50) NOT NULL UNIQUE,
    descripcion VARCHAR(150) NULL,
    orden_visual SMALLINT NOT NULL DEFAULT 1,
    activa BIT NOT NULL DEFAULT 1
);

CREATE TABLE productos (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    id_categoria SMALLINT NOT NULL,
    nombre_producto VARCHAR(100) NOT NULL UNIQUE,
    descripcion VARCHAR(255) NULL,
    imagen_producto VARCHAR(255) NULL,
    precio_actual DECIMAL(10,2) NOT NULL CHECK (precio_actual >= 0),
    disponible BIT NOT NULL DEFAULT 1,
    activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_productos_categorias FOREIGN KEY (id_categoria) REFERENCES categorias(id_categoria)
);

CREATE TABLE clientes (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre_cliente VARCHAR(100) NOT NULL,
    telefono VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE direccion_cliente (
    id_direccion INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    referencia VARCHAR(255) NULL,
    activa BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_direccion_cliente FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente)
);

CREATE TABLE estados_orden (
    id_estado SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre_estado VARCHAR(50) NOT NULL UNIQUE,
    orden_flujo SMALLINT NOT NULL,
    estado_final BIT NOT NULL DEFAULT 0
);

CREATE TABLE tipos_entrega (
    id_tipo_entrega SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre_tipo VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE metodos_pago (
    id_metodo_pago SMALLINT IDENTITY(1,1) PRIMARY KEY,
    nombre_metodo VARCHAR(50) NOT NULL UNIQUE,
    requiere_referencia BIT NOT NULL DEFAULT 0
);

-- 3. Crear Tablas Transaccionales
CREATE TABLE ordenes (
    id_orden BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_estado SMALLINT NOT NULL,
    id_tipo_entrega SMALLINT NOT NULL,
    id_usuario INT NOT NULL,
    id_cliente INT NULL,
    id_direccion INT NULL,
    fecha_hora DATETIME NOT NULL DEFAULT GETDATE(),
    observacion_general VARCHAR(255) NULL,
    CONSTRAINT FK_ordenes_estados FOREIGN KEY (id_estado) REFERENCES estados_orden(id_estado),
    CONSTRAINT FK_ordenes_tipos_entrega FOREIGN KEY (id_tipo_entrega) REFERENCES tipos_entrega(id_tipo_entrega),
    CONSTRAINT FK_ordenes_usuarios FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario),
    CONSTRAINT FK_ordenes_clientes FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente),
    CONSTRAINT FK_ordenes_direcciones FOREIGN KEY (id_direccion) REFERENCES direccion_cliente(id_direccion)
);

CREATE TABLE detalle_orden (
    id_detalle BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_orden BIGINT NOT NULL,
    id_producto INT NOT NULL,
    cantidad SMALLINT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL CHECK (precio_unitario >= 0),
    CONSTRAINT FK_detalle_ordenes FOREIGN KEY (id_orden) REFERENCES ordenes(id_orden) ON DELETE CASCADE,
    CONSTRAINT FK_detalle_productos FOREIGN KEY (id_producto) REFERENCES productos(id_producto)
);

CREATE TABLE indicaciones_detalle (
    id_indicacion BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_detalle BIGINT NOT NULL,
    indicacion VARCHAR(150) NOT NULL,
    CONSTRAINT FK_indicaciones_detalle FOREIGN KEY (id_detalle) REFERENCES detalle_orden(id_detalle) ON DELETE CASCADE
);

CREATE TABLE pagos_ventas (
    id_pago BIGINT IDENTITY(1,1) PRIMARY KEY,
    id_orden BIGINT NOT NULL UNIQUE,
    id_metodo_pago SMALLINT NOT NULL,
    fecha_hora_pago DATETIME NOT NULL DEFAULT GETDATE(),
    monto_aplicado DECIMAL(10,2) NOT NULL CHECK (monto_aplicado >= 0),
    monto_recibido DECIMAL(10,2) NOT NULL CHECK (monto_recibido >= 0),
    referencia_pago VARCHAR(100) NULL,
    CONSTRAINT FK_pagos_ordenes FOREIGN KEY (id_orden) REFERENCES ordenes(id_orden),
    CONSTRAINT FK_pagos_metodos FOREIGN KEY (id_metodo_pago) REFERENCES metodos_pago(id_metodo_pago)
);

-- 4. Crear Índices de Optimización
CREATE INDEX IX_ordenes_estado_fecha ON ordenes(id_estado, fecha_hora);
CREATE INDEX IX_ordenes_cliente ON ordenes(id_cliente);
CREATE INDEX IX_detalle_orden ON detalle_orden(id_orden);
CREATE INDEX IX_productos_categoria ON productos(id_categoria, activo);

-- 5. Cargar Datos Iniciales (Seed Data)
INSERT INTO roles (nombre_rol, descripcion) VALUES 
('Administrador', 'Control total del sistema'),
('Cajero', 'Cobro e impresión de tickets'),
('Mesero/Recepcion', 'Toma de pedidos'),
('Cocina', 'Visualización y preparación de pedidos');

INSERT INTO estados_orden (nombre_estado, orden_flujo, estado_final) VALUES 
('Pendiente', 1, 0),
('En Preparación', 2, 0),
('Listo', 3, 0),
('Entregado', 4, 1),
('Cancelado', 5, 1);

INSERT INTO tipos_entrega (nombre_tipo) VALUES 
('Comer Aquí'),
('Para Llevar'),
('Domicilio');

INSERT INTO metodos_pago (nombre_metodo, requiere_referencia) VALUES 
('Efectivo', 0),
('Tarjeta', 1),
('Transferencia', 1);

INSERT INTO categorias (nombre_categoria, descripcion, orden_visual) VALUES 
('HAMBURGUESAS', 'Hamburguesas clásicas y especiales', 1),
('ESPECIALIDADES', 'Platillos especiales de la casa', 2),
('BEBIDAS', 'Refrescos, sodas y bebidas', 3),
('EXTRAS', 'Acompañamientos y complementos', 4);

INSERT INTO productos (id_categoria, nombre_producto, descripcion, precio_actual, disponible, activo) VALUES 
(1, 'Bumburys Clásica', 'Hamburguesa tradicional con queso', 4.50, 1, 1),
(1, 'Bumburys Doble', 'Doble carne y doble queso', 8.00, 1, 1),
(3, 'Soda', 'Bebida fría 500ml', 4.50, 1, 1),
(4, 'Papas Fritas', 'Porción de papas crujientes', 2.00, 1, 1),
(4, 'Salsa', 'Porción extra de salsa', 1.00, 0, 1);

INSERT INTO usuarios (id_rol, nombre_completo, nombre_usuario, contrasena) VALUES 
(1, 'Admin Bumburys', 'admin', 'admin123');