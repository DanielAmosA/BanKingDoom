# Daniel Amos Full Stack Project 💻🌟

Welcome to the **Full Stack Project** by Daniel Amos! 🚀 This project involves building a **Banking System** for deposit and withdrawal operations, 
including a seamless front-end and back-end architecture. 
The system interacts with a third-party provider to simulate real-life banking operations.

## Technologies Used ⚙️

### Frontend
- **ReactJS** ⚛️
- **HTML5**, **CSS3**, **JavaScript** 🎨

### Backend
- **.NET Core** 💻
- **RESTful API** for interactions

### Database
- **SQL Server** 🗃️

## Project Structure 📁

/Daniel Amos Project │ ├── Backend │ └── Contains .NET Core Web API for handling banking operations. │ ├── DB │ └── Database models and tables for transaction history. │ ├── Frontend │ └── ReactJS frontend for handling user input and displaying the transaction status.

## Installation Guide 🛠️

### 1. **Clone the repository**
   Start by cloning this repository to your local machine:
   ```bash
   git clone https://github.com/danielamos/fullstack-project.git

2. Backend Setup (NET Core)
Navigate to the Backend folder.
Open the project in Visual Studio or your preferred .NET Core IDE.
Run the application:
dotnet run

3. Frontend Setup (ReactJS)
Navigate to the Frontend folder.
Install dependencies:
npm install
Run the React development server:
npm start

4. Database Configuration (SQL Server)
Ensure you have SQL Server installed.
Use the connection string located in appsettings.json for database access:
"DbConfig": {
  "ConnectionString": "Server=DESKTOP-RJQ201T;Database=DanielAmosDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Create the database and apply the required migrations using your SQL management tool.
5. Test the Application
Once both backend and frontend are up and running, open the frontend in a browser and test deposit and withdrawal operations.
You can use State Management (e.g., Redux or Vuex) to handle the transaction status and history dynamically.

Key Features 🔑
Deposit and Withdrawal operations with data validation.
Transaction History: View all performed operations.
Edit and Cancel Transactions (Bonus Features).
Real-time Updates using state management.

Available API Endpoints 🔌
Create Token:
URL: https://openBanking/createtoken
Params: userId, SecretId

Deposit Operation:
URL: https://openBanking/createdeposit
Params: amount, bank

Withdrawal Operation:
URL: https://openBanking/createWithdrawal
Params: amount, bank

Important Notes ⚠️
Make sure the SQL Server is running and the connection string is correct.
The system supports both deposit and withdrawal operations.
You can test the features locally with minimal external dependencies.
License 📄
This project is open source and available under the MIT License.

Thanks for using the Daniel Amos Full Stack Project! 🎉
If you have any issues or improvements, feel free to contribute or reach out. Let's build something great! 🚀✨
