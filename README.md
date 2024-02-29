# Persist_Excel_Upload_toSqlDb

This repo contains (3) .xlsx files for testing Excel file upload.  It uses a static generic method to deserialize the rows into a List<T>.  The T object only needs to have matching header names.

You can test excel file upload via the API without having to create the SQL DB & persist the data.  Just select [testDeserializationOnly] = true in the swagger POST action.

If you want to create the SQL Db & persist the upload data, select the DAL project in the Package Manager Console and run "Update-Database" on the command line (see pic below).

Validate persistence/upload using the GET Methods.  

Note: There is a unique constraint on OrderNumber.

![image](https://github.com/jcmusic/Persist_Excel_Upload_toSqlDb/assets/1017378/3afd3d06-65dc-4d44-a352-afdfe242aab0)
