-- Create Database
CREATE DATABASE QuantityMeasurementDB;
GO

USE QuantityMeasurementDB;
GO

-- Create Table
CREATE TABLE QuantityHistory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Category NVARCHAR(50) NOT NULL,         
    OperationType NVARCHAR(50) NOT NULL,         
    FirstValue DECIMAL(18,4) NULL,
    FirstUnit NVARCHAR(20) NULL,
    SecondValue DECIMAL(18,4) NULL,
    SecondUnit NVARCHAR(20) NULL,
    TargetUnit NVARCHAR(20) NULL,               
    ResultValue DECIMAL(18,4) NULL,
    ResultUnit NVARCHAR(20) NULL,
    ErrorMessage NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) DEFAULT SYSTEM_USER,
    ExecutionTimeMs INT NULL
);


select * from dbo.QuantityHistory;

-- Create Indexes
CREATE INDEX IX_QuantityHistory_Category ON QuantityHistory(Category);
CREATE INDEX IX_QuantityHistory_OperationType ON QuantityHistory(OperationType);
CREATE INDEX IX_QuantityHistory_CreatedAt ON QuantityHistory(CreatedAt);
GO

-- Stored Procedure: Insert
CREATE PROCEDURE SPInsert_Quantity_History
    @Category NVARCHAR(50),
    @OperationType NVARCHAR(50),
    @FirstValue DECIMAL(18,4) = NULL,
    @FirstUnit NVARCHAR(20) = NULL,
    @SecondValue DECIMAL(18,4) = NULL,
    @SecondUnit NVARCHAR(20) = NULL,
    @TargetUnit NVARCHAR(20) = NULL,
    @ResultValue DECIMAL(18,4) = NULL,
    @ResultUnit NVARCHAR(20) = NULL,
    @ErrorMessage NVARCHAR(MAX) = NULL,
    @ExecutionTimeMs INT = NULL,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO QuantityHistory (
        Category, OperationType, FirstValue, FirstUnit,
        SecondValue, SecondUnit, TargetUnit,
        ResultValue, ResultUnit, ErrorMessage, ExecutionTimeMs
    )
    VALUES (
        @Category, @OperationType, @FirstValue, @FirstUnit,
        @SecondValue, @SecondUnit, @TargetUnit,
        @ResultValue, @ResultUnit, @ErrorMessage, @ExecutionTimeMs
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- Stored Procedure: Get All
CREATE PROCEDURE SPGet_All_Quantity_History
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Category, OperationType,
        FirstValue, FirstUnit,
        SecondValue, SecondUnit,
        TargetUnit,
        ResultValue, ResultUnit,
        ErrorMessage, ExecutionTimeMs, CreatedAt, CreatedBy
    FROM QuantityHistory
    ORDER BY CreatedAt DESC;
END
GO

-- Stored Procedure: Get By ID
CREATE PROCEDURE SPGet_Quantity_History_By_Id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Category, OperationType,
        FirstValue, FirstUnit,
        SecondValue, SecondUnit,
        TargetUnit,
        ResultValue, ResultUnit,
        ErrorMessage, ExecutionTimeMs, CreatedAt, CreatedBy
    FROM QuantityHistory
    WHERE Id = @Id;
END
GO

-- Stored Procedure: Get By Category
CREATE PROCEDURE SPGet_Quantity_History_By_Category
    @Category NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Category, OperationType,
        FirstValue, FirstUnit,
        SecondValue, SecondUnit,
        TargetUnit,
        ResultValue, ResultUnit,
        ErrorMessage, ExecutionTimeMs, CreatedAt, CreatedBy
    FROM QuantityHistory
    WHERE Category = @Category
    ORDER BY CreatedAt DESC;
END
GO

-- Stored Procedure: Get By Operation Type
CREATE PROCEDURE SPGet_Quantity_History_By_Operation_Type
    @OperationType NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id, Category, OperationType,
        FirstValue, FirstUnit,
        SecondValue, SecondUnit,
        TargetUnit,
        ResultValue, ResultUnit,
        ErrorMessage, ExecutionTimeMs, CreatedAt, CreatedBy
    FROM QuantityHistory
    WHERE OperationType = @OperationType
    ORDER BY CreatedAt DESC;
END
GO

-- Stored Procedure: Delete By ID
CREATE PROCEDURE SPDelete_Quantity_History_By_Id
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM QuantityHistory WHERE Id = @Id;
    RETURN @@ROWCOUNT;
END
GO

-- Stored Procedure: Delete All
CREATE PROCEDURE SPDelete_All_Quantity_History
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM QuantityHistory;
    RETURN @@ROWCOUNT;
END
GO

-- Stored Procedure: Get Count
CREATE PROCEDURE SPGet_Quantity_History_Count
AS
BEGIN
    SELECT COUNT(*) FROM QuantityHistory;
END
GO