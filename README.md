# ThunderWings

This is a RESTful API for an e-commerce application that sells aircraft. It provides endpoints for searching aircraft, adding/removing basket items, and checkout operations.

## Features

- Get all aircraft with optional filtering parameters 
- Add an aircraft to the basket
- Remove an aircraft from the basket
- Checkout basket 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 8.0 or later

### Installing

1. Clone the repository

### Built with 

- EntityFramework Core
- SQLite
- AutoMapper
- xUnit/Moq

### Please note

- Paginiation is defaulted to page number 1 and a page size of 10
- The solution uses an SQLite db in the root of the API project. If the database is removed it will be recreated and re-seeded with aircraft data on startup. 
