# DHDCShop
### This is a Shoe Store Web 
**Technicality**: ASP.NET MVC <br/>
**Library**: Jquery, SignalR, Bootstrap, CKEditor...
**Database**: SQL server <br/>
**Features**: All base features of a store<br/>
  + *Client*: 
    - Register, Login and authorize by email
    - Filter and search product
    - Wishlist management
    - Product ordering (Online payment via VnPay (testEnvironment))
    - Order statuses management
    - Review product
    - Read blog
    - Contact and Chat with admin
  + *Admin*: 
    - Statistics dashboard
    - Order management
    - User management
    - Product management
    - Blog management
    - Chat management 
    - Contact management 
## How to use it? 
## How to Run the ASP.NET MVC Project

### Steps to Follow:

1. **Clone or download the project**:
   - Clone the repository with the following command:
     ```bash
     git clone <repository-url>
     ```
   - Or download the project from the repository and extract it.

2. **Install dependencies**:
   - Open the terminal or command prompt in the root folder of the project.
   - Run the following command to restore the necessary packages:
     ```bash
     dotnet restore
     ```

3. **Update the database**:
   - Run the following command to apply migrations and update the database:
     ```bash
     dotnet ef database update --project DHDCShop.Models
     ```

4. **Run the project**:
   - Run the project with the following command:
     ```bash
     dotnet run
     ```

5. **Access the application**:
   - Once the project is running, open your browser and navigate to the provided URL.

### Requirements:
- **.NET Core SDK** must be installed on your machine to use CLI commands like `dotnet`.
- Ensure your database configuration in `appsettings.json` or the relevant configuration file is correct, if needed.

#### To use online payment feature, you have to follow example credit cards in this links: https://sandbox.vnpayment.vn/apis/vnpay-demo/

