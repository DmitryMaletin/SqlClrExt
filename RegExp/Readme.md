Simple CLR function to overcome lack of regular expression in SQL SERVER. 

![learn-regex](https://github.com/DmitryMaletin/learn-regex)

Examples: 
```
-- Validate ip adress 
DECLARE @ipCheckRegex nvarchar(4000) = '^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$'

SELECT ip ,isMatched FROM (
VALUES ('000.0000.00.00'),('192.168.1.1'),('276.168.11.1'),('192.168.222.256')) ips(ip)
OUTER APPLY [dbo].[tvf_RegExIsMatch](ip,@ipCheckRegex)

-- validate email 

DECLARE @emailCheckRegex nvarchar(4000) = '^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$'
SELECT email, isMatched FROM (
VALUES ('test@gmail.com'),('test.test.test'),('test$gmail.com'),('test#@yahoo.com')) ips(email)
OUTER APPLY [dbo].[tvf_RegExIsMatch](email,@emailCheckRegex)


```
