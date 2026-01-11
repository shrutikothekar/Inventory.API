# Inventory.API
**Backend** 

✅ ADO.NET + SQL — Learned Till Now

1️⃣ Database Connectivity
SqlConnection
Connection string from appsettings.json
Open / Close connection properly

2️⃣ Command Execution
SqlCommand
CommandType.StoredProcedure
CommandType.Text
Parameterized queries (SqlParameter)
Prevent SQL Injection

3️⃣ Stored Procedures
Create & alter stored procedures
Pass input parameters
Return result sets
Handle multiple result sets
Performance optimization (indexes, joins)

4️⃣ Reading Data
SqlDataReader
Forward-only, fast read
Mapping DB rows → Model classes

5️⃣ Transactions
SqlTransaction
Commit / Rollback
Handling partial failures
Real-life example:
Insert Product
Update Order
Insert OrderTransaction
Rollback on error

6️⃣ ACID Concepts (Practical)
Atomicity → All or nothing
Consistency → Valid state after transaction
Isolation → No dirty data visible
Durability → Committed data persists
(Especially Isolation with real meaning ✔)

7️⃣ Bulk Operations
SqlBulkCopy
Upload large CSV data
BatchSize
TableLock
Transactions with bulk insert
High-volume performance handling

8️⃣ Indexing
UNIQUE INDEX
NONCLUSTERED INDEX
When & why to use indexes
Index impact on read vs write

9️⃣ Error Handling
try-catch-finally
SQL exceptions
Centralized exception middleware
Meaningful error responses

🔟 Repository Pattern
BaseRepository
Separate data access logic
Clean architecture
Reusability

1️⃣1️⃣ Dependency Injection (DI)
Register services (Scoped, Singleton)
Interface → Implementation
DbConnectionFactory pattern
Fixing DI resolution errors

1️⃣2️⃣ Configuration Management
appsettings.json
IConfiguration
Environment-based configs

1️⃣3️⃣ Performance Thinking (Very Important)
Why not LINQ for bulk
Why SqlBulkCopy is faster
When to use EF vs ADO.NET

