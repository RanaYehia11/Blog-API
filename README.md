# Blog API  

A **RESTful API** built with **ASP.NET Core** for managing **Authors, Posts, and Comments**.  
It uses **Entity Framework Core** with **SQL Server** for database operations.  

---

## Features
- Author Management → Create, update, delete, and list authors  
- Blog Post Management → Full CRUD operations with relationships to authors  
- Comment Management → Add and manage comments linked to posts  
- Database Integration → Code-first migrations powered by EF Core  
- API Documentation → Swagger/OpenAPI support for testing and exploration  

---

## Tech Stack
- ASP.NET Core
- Entity Framework Core  
- SQL Server  
- Swagger / OpenAPI  

---

## API Endpoints  

# Author Management
- GET /api/authors → Get all authors  
- GET /api/authors/{id} → Get author by ID  
- GET /api/authors/{id}/posts → Get all posts by a specific author  
- POST /api/authors/add → Add a new author  
- PUT /api/authors/update/{id} → Update an author  
- DELETE /api/authors/delete/{id} → Delete an author  

---

# Post Management
- GET /api/posts → Get all posts  
- GET /api/posts/{id} → Get post by ID  
- GET /api/posts/{postId}/comments → Get all comments for a post  
- POST /api/posts/add → Create a new post  
- PUT /api/posts/update/{id} → Update an existing post  
- DELETE /api/posts/delete/{id} → Delete a post  

---

# Comment Management
- GET /api/comments → Get all comments  
- GET /api/comments/{id} → Get comment by ID  
- POST /api/comments/add → Add a comment  
- PUT /api/comments/update/{id} → Update a comment  
- DELETE /api/comments/delete/{id} → Delete a comment  
