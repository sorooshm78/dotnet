-- mysql -u username -p database_name < path/to/your/SqlScript.sql
-- sqlcmd -U myLogin -P myPassword -S MyServerName -d MyDatabaseName -Q "DROP TABLE MyTable"
-- sqlcmd -U myLogin -P myPassword -S MyServerName -d MyDatabaseName -i SqlScript.sql

use SchoolDb;
select LastName from Students where FirstName='Harry';
