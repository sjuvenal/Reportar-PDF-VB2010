CREATE DATABASE DB_PDF

---crear tabla
create table producto
(
proid int primary key identity(1,1),
procod varchar(50),
pronom varchar(50),
procat varchar(50),
propre money,
prosto int,
prouni varchar(30),
proimg varchar(max)
)

--insertar registros
INSERT [dbo].[producto] ([procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES ( N'pro01', N'leche', N'latios', 2.0000, 12, N'unidad', N'C:\imgprod\pro01.png')
INSERT [dbo].[producto] ([procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES ( N'pro02', N'arroz', N'sereales', 4.0000, 2, N'kilo', N'C:\imgprod\pro02.png')
INSERT [dbo].[producto] ( [procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES (N'pro03', N'azucar', N'dulce', 3.0000, 15, N'kilo', N'C:\imgprod\pro03.png')
INSERT [dbo].[producto] ( [procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES (N'pro04', N'fideo', N'serales', 2.0000, 15, N'bolza', N'C:\imgprod\pro04.png')
INSERT [dbo].[producto] ([procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES ( N'pro05', N'lentejas', N'latios', 3.0000, 12, N'kilos', N'C:\imgprod\pro05.png')
INSERT [dbo].[producto] ([procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES ( N'pro06', N'ariel', N'detergentes', 4.0000, 7, N'bolsas', N'C:\imgprod\pro06.png')
INSERT [dbo].[producto] ( [procod], [pronom], [procat], [propre], [prosto], [prouni], [proimg]) VALUES ( N'pro07', N'tomate', N'verduras', 5.0000, 8, N'kilos', N'C:\imgprod\pro07.png')

--consultar registros
select proid Id,procod codigo,pronom Producto,procat 
Categoria,prouni Unidad,propre Precio,prosto stock,
proimg Imagen from producto