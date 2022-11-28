IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221126092409_DHPRODUCTS')
BEGIN
    CREATE TABLE [Images] (
        [ProductId] nvarchar(450) NOT NULL,
        [ProductName] nvarchar(MAX) NULL,
        [ProductPrice] float NOT NULL,
        [ProductDetail] nvarchar(MAX) NULL,
        [ProductSlot] nvarchar(100) NULL,
        [Series] nvarchar(MAX) NULL,
        [Description] nvarchar(MAX) NULL,
        [ProductSize] nvarchar(100) NULL,
        [ProductRam] nvarchar(100) NULL,
        [ProductGPU] nvarchar(100) NULL,
        [ProductPOWER] nvarchar(100) NULL,
        [ProductImage] nvarchar(100) NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([ProductId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221126092409_DHPRODUCTS')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221126092409_DHPRODUCTS', N'3.1.30');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221127151736_DHCART')
BEGIN
    CREATE TABLE [CartItems] (
        [Id] int NOT NULL IDENTITY,
        [imageProductId] nvarchar(450) NULL,
        [Quantity] real NOT NULL,
        [VGAId] nvarchar(max) NULL,
        CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CartItems_Images_imageProductId] FOREIGN KEY ([imageProductId]) REFERENCES [Images] ([ProductId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221127151736_DHCART')
BEGIN
    CREATE INDEX [IX_CartItems_imageProductId] ON [CartItems] ([imageProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221127151736_DHCART')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221127151736_DHCART', N'3.1.30');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221128131602_CartAdded')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [OrderTotal] real NOT NULL,
        [OrderPlaced] datetime2 NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221128131602_CartAdded')
BEGIN
    CREATE TABLE [OrderItems] (
        [Id] int NOT NULL IDENTITY,
        [Quantity] real NOT NULL,
        [Price] real NOT NULL,
        [OrderId] int NOT NULL,
        [VGAId] nvarchar(max) NULL,
        [VGAImage] nvarchar(max) NULL,
        [VGAName] nvarchar(max) NULL,
        [ImageProductId] nvarchar(450) NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItems_Images_ImageProductId] FOREIGN KEY ([ImageProductId]) REFERENCES [Images] ([ProductId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221128131602_CartAdded')
BEGIN
    CREATE INDEX [IX_OrderItems_ImageProductId] ON [OrderItems] ([ImageProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221128131602_CartAdded')
BEGIN
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221128131602_CartAdded')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221128131602_CartAdded', N'3.1.30');
END;

GO

