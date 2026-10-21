CREATE TABLE usuarios_usss004826 (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    usuario VARCHAR(35),
    clave VARCHAR(35),
    nombre VARCHAR(65),
    direccion VARCHAR(85),
    telefono VARCHAR(9)
);

INSERT INTO usuarios_usss004826 (usuario, clave, nombre, direccion, telefono)
VALUES 
('admin123', 'Clave123', 'Administrador Principal', 'Usulután', '7777-7777'),
('valeria07', 'Valeria2026', 'Valeria Solano', 'Usulután', '6666-6666'),
('usuario_03', 'User03DB', 'Carlos Perez', 'San Miguel', '2222-2222');