ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [Test], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

