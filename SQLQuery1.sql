

SET IDENTITY_INSERT Types ON;

INSERT INTO Types (TypeId, NameType, Created_at, Created_by, Updated_at, Updated_by) VALUES
(1, 'Electronics', '2023-05-28 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin'),
(2, 'Furniture', '2023-04-28 00:00:00', 'Admin', '2023-06-17 00:00:00', 'Admin'),
(3, 'Clothing', '2023-06-07 00:00:00', 'Admin', '2023-06-22 00:00:00', 'Admin'),
(4, 'Books', '2023-05-18 00:00:00', 'Admin', '2023-06-25 00:00:00', 'Admin'),
(5, 'Beauty', '2023-05-08 00:00:00', 'Admin', '2023-06-23 00:00:00', 'Admin'),
(6, 'Toys', '2023-06-03 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin'),
(7, 'Sports', '2023-06-13 00:00:00', 'Admin', '2023-06-22 00:00:00', 'Admin'),
(8, 'Automotive', '2023-05-03 00:00:00', 'Admin', '2023-06-19 00:00:00', 'Admin');

SET IDENTITY_INSERT Types OFF;

SET IDENTITY_INSERT Products ON;

INSERT INTO Products (Id, Code, Name, Description, Price, Created_at, Created_by, Updated_at, Updated_by, TypeId) VALUES
(1, 'P001', 'Laptop', 'A high performance laptop', 1500, '2023-05-28 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin',1),
(2, 'P002', 'Smartphone', 'A latest model smartphone', 1000, '2023-04-28 00:00:00', 'Admin', '2023-06-17 00:00:00', 'Admin',2),
(3, 'P003', 'Tablet', 'A lightweight tablet', 800, '2023-06-07 00:00:00', 'Admin', '2023-06-22 00:00:00', 'Admin',3),
(4, 'P004', 'Monitor', 'A 24-inch monitor', 200, '2023-05-18 00:00:00', 'Admin', '2023-06-25 00:00:00', 'Admin',2),
(5, 'P005', 'Keyboard', 'A mechanical keyboard', 100, '2023-05-08 00:00:00', 'Admin', '2023-06-23 00:00:00', 'Admin',6),
(6, 'P006', 'Mouse', 'A wireless mouse', 50, '2023-06-03 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin',4),
(7, 'P007', 'Printer', 'An all-in-one printer', 300, '2023-06-13 00:00:00', 'Admin', '2023-06-22 00:00:00', 'Admin',8),
(8, 'P008', 'Scanner', 'A high-resolution scanner', 250, '2023-05-03 00:00:00', 'Admin', '2023-06-19 00:00:00', 'Admin',7),
(9, 'P009', 'Headphones', 'Noise-cancelling headphones', 150, '2023-04-28 00:00:00', 'Admin', '2023-06-18 00:00:00', 'Admin',6),
(10, 'P010', 'Speaker', 'A Bluetooth speaker', 120, '2023-05-30 00:00:00', 'Admin', '2023-06-25 00:00:00', 'Admin',3),
(11, 'P011', 'External Hard Drive', '1TB external hard drive', 80, '2023-04-28 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin',5),
(12, 'P012', 'Router', 'A high-speed router', 100, '2023-05-03 00:00:00', 'Admin', '2023-06-23 00:00:00', 'Admin',8),
(13, 'P013', 'Webcam', 'A 1080p webcam', 70, '2023-05-08 00:00:00', 'Admin', '2023-06-25 00:00:00', 'Admin',5),
(14, 'P014', 'Smart Watch', 'A smart watch with health tracking', 200, '2023-05-28 00:00:00', 'Admin', '2023-06-26 00:00:00', 'Admin',4),
(15, 'P015', 'Gaming Console', 'A popular gaming console', 500, '2023-05-18 00:00:00', 'Admin', '2023-06-22 00:00:00', 'Admin',1);

SET IDENTITY_INSERT Products OFF;

SELECT * FROM Products
Select * From AspNetUsers

SELECT * FROM Types

