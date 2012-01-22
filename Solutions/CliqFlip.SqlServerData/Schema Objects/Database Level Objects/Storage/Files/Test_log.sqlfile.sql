ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [Test_log], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Test_log.ldf', SIZE = 1792 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);



