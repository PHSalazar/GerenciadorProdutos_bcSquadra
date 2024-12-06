CREATE TABLE [dbo].[Produtos]
(
	[Id] INT NOT NULL IDENTITY, 
    [Nome] VARCHAR(50) NOT NULL, 
    [Descricao] VARBINARY(50) NOT NULL, 
    [Categoria] INT NOT NULL, 
    [Preco] DECIMAL(18, 2) NOT NULL,
    [StatusEstoque] BIT NOT NULL, 
    [QuantidadeEstoque] INT NOT NULL
)