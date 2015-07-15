# FileSplitter
Small command-line tool to break huge text files into smaller ones based on predetermined amount of lines in each partition

Usage: FileSplitter input-file-path number-of-lines-in-each-partition prefix-for-partition-name

It will take input text file and break it into a set of "partitions", each no more than number-of-lines-in-each-partition lines. I needed it to be able to work with huge auto-generated SQL scripts that wouldn't open in Notepad++ or SQL Management Studio due to their size - you may find it useful for something else. 

Written for .NET 4.5 - however it is very simple and will work for lower .NET Framework versions.
