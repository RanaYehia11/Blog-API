Blog API

A RESTful Blog API built with **ASP.NET Core**.  
It provides CRUD operations for managing **Posts, Authors, and Comments**, using **Entity Framework Core** with **SQL Server** as the database.  
The API is documented with **Swagger/OpenAPI**.

---

##  Features
-  Manage Authors (Add, Update, Delete, List)  
-  Manage Blog Posts (CRUD operations with Author relationship)  
-  Manage Comments on Posts  
-  Database migrations with EF Core  
   

---

## Tech Stack
- **ASP.NET Core (.NET 8)**
- **Entity Framework Core**
- **SQL Server** 
- **Swagger / OpenAPI**

  ##  API Endpoints

###  Author Management
- GET /api/authors` → Retrieve all authors  
- GET /api/authors/{id}` → Fetch a single author by ID  
- GET /api/authors/{id}/posts` → View all posts created by a specific author  
- POST /api/authors/add` → Add a new author  
- PUT /api/authors/update/{id}` → Edit author details  
- DELETE /api/authors/delete/{id}` → Remove an author  

---

###  Post Management
- GET /api/posts` → Retrieve all posts  
- GET /api/posts/{id}` → Fetch details of a single post  
- GET /api/posts/{postId}/comments` → Get all comments for a given post  
- POST /api/posts/add` → Create a new blog post  
- PUT /api/posts/update/{id}` → Update an existing post  
- DELETE /api/posts/delete/{id}` → Delete a post  

---

###  Comment Management
- GET /api/comments` → Retrieve all comments  
- GET /api/comments/{id}` → Fetch a specific comment by ID  
- POST /api/comments/add` → Add a comment to a post  
- PUT /api/comments/update/{id}` → Modify a comment  
- DELETE /api/comments/delete/{id}` → Delete a comment  
