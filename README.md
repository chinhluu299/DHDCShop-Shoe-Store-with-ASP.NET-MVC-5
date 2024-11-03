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

### Some images:
![Picture1](https://github.com/user-attachments/assets/c24d4f51-1b4c-4f28-9bd7-16dff9092896)
![Picture2](https://github.com/user-attachments/assets/1abe964f-fab6-4932-9f3b-0a44be573881)
![Picture3](https://github.com/user-attachments/assets/91aac159-a5b1-4cb6-9d47-a12627041b81)
![Picture4](https://github.com/user-attachments/assets/541df836-1330-4b06-b2c0-1d5c21a45a9c)
![Picture5](https://github.com/user-attachments/assets/dd68acf8-d85e-4a88-92bf-2741ae4432d3)
![Picture6](https://github.com/user-attachments/assets/5156b58b-5e0e-492f-96f2-b28b59c6fd0b)
![Picture7](https://github.com/user-attachments/assets/39a3d788-cbb9-4bb1-ba83-98875f947996)
![Picture8](https://github.com/user-attachments/assets/867b6656-de0a-4f06-b5f9-626863ac0a7d)
![Picture9](https://github.com/user-attachments/assets/a0d3faa3-b1da-4f24-a7f4-aea28ff74ed0)
![Picture10](https://github.com/user-attachments/assets/460dd9ed-d53f-46dc-a795-c258302d1624)
![Picture11](https://github.com/user-attachments/assets/00941aff-da0e-46be-9b9e-4b02e2024c25)
![Picture12](https://github.com/user-attachments/assets/f1abb467-50ba-49af-a486-a8b81be10b17)
![Picture13](https://github.com/user-attachments/assets/3df889df-3dcf-4ef7-bf87-14d2c9713849)
![Picture14](https://github.com/user-attachments/assets/43cf85b4-60af-48bd-a281-9c4204f90abf)
![Picture15](https://github.com/user-attachments/assets/2fb1593b-1b29-4941-9c47-b73f34c759d5)

