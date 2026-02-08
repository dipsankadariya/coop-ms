# Coop-ms 

### 1. User Registration (/Account/Register)
- New user enters Username, Email, Password,.
- Password is hashed using BCrypt before storing in Users table
- User account created in database

### 2. User Login (/Account/Login)
- User enters Username and Password
- System verifies credentials using BCrypt
- On success: Creates authentication cookie with Claims (UserId, Username, Role)
- Cookie expires after 24 hours
- Redirects to Home page

### 3. Authorization 
- All controllers are protected with [Authorize] attribute
- User must be logged in to access any features
- User info displayed in navbar with role and logout option

---

## 👥 Member Management Flow

### 4. View Members (/Member/Index)
- Displays all members in a table
- Shows: FullName, Email, Address, Phone, Status (Active/Inactive badge)
- Actions: Update, Delete buttons for each member

### 5. Add Member (/Member/Create)
- Form with: FullName, Email, Phone, Address, Status
- Data flow:
  - Controller → MemberVm (ViewModel)
  - Mapper → MemberDto (Data Transfer Object)
  - Service → Member (Entity)
  - Repository → Database

### 6. Update Member (/Member/Update/{id})
- Loads existing member data
- Updates: FullName, Email, Phone, Address, Status
- Cannot change MemberId

### 7. Delete Member (/Member/Delete/{id})
- Confirmation page shows member details
- Permanently removes member from database

---

## 💰 Member Share Management Flow

### 8. Manage Member Shares (/MemberShare/Index)
- Lists all members with their share information
- Shows: Member name, Total Shares, Share Amount
- Actions: "Add Share" button for each member

### 9. Add Share (/MemberShare/AddShare?memberId={id})
- Displays member context (name, email)
- Form fields: ShareType, NumberOfShares, ShareAmount
- Automatically calculates TotalAmount = NumberOfShares × ShareAmount
- Creates new share record linked to member

### 10. View Share Details (/MemberShare/ViewMemberSharesDetails?memberId={id})
- Shows all shares for a specific member
- Displays: ShareType, Number of Shares, Share Amount, Purchase Date
- Running total of all shares

---

## 🏦 Account Management Flow (Latest Feature)

### 11. Manage Accounts (/MemberAccount/Index)
- Shows only Active members in the list
- Two action buttons per member:
  - View Accounts - See all accounts for this member
  - Create Account - Add new account for this member

### 12. Create Account (/MemberAccount/Create?memberId={id})
- Shows member context at top (name, email, phone)
- Form fields:
  - AccountType: Dropdown (Savings, Current, Fixed Deposit, Loan)
  - Initial Balance: Number input
  - Status: Dropdown (Active, Inactive)
- Business rule: Can only create accounts for Active members
- On success: Redirects to ViewAccounts for that member
- Data includes: CreatedAt timestamp (auto-set to current date)

### 13. View Member Accounts (/MemberAccount/ViewAccounts?memberId={id})
- Shows member name at top
- Table displays all accounts:
  - Account ID
  - Account Type
  - Balance (₹ formatted)
  - Status (color-coded badge)
  - Created At (formatted date)
  - Update Status button
- "Add New Account" button at top
- Alert messages show in fixed-height container

### 14. Update Account Status (/MemberAccount/Update?accountId={id})
- Shows member name and account details (ID, Type, Balance)
- Can only change Status (Active ↔ Inactive)
- Business Rule Enforcement:
  - Warning displayed if balance ≠ 0
  - Cannot deactivate account with non-zero balance
  - Server-side validation prevents this
- Balance, AccountType, and MemberId are read-only

---

## 💸 Account Transaction Management Flow

### 15. Add Transaction (/AccountTransaction/AddTransaction?accountId={id}&transactionType={type})
- Select member from dropdown (active members only)
- Accounts dropdown dynamically loads based on selected member
- Shows only active accounts with current balance
- Form fields:
  - Member: Dropdown (auto-populated if coming from account view)
  - Account: Dropdown showing AccountType and current Balance
  - TransactionType: Deposit or Withdrawal
  - Amount: Number input
  - Notes: Optional text field
- Business rules:
  - Only active accounts can have transactions
  - Withdrawal cannot exceed current balance
  - BalanceAfter automatically calculated
- On success: Redirects to ViewStatement for that account

### 16. Select Statement (/AccountTransaction/SelectStatement)
- Select a member from dropdown
- Accounts dropdown loads dynamically via AJAX
- Submit to view transaction history for selected account

### 17. View Statement (/AccountTransaction/ViewStatement?accountId={id})
- Displays member name, account type, and current balance at top
- Table shows all transactions:
  - Transaction ID
  - Transaction Type (Deposit/Withdrawal)
  - Amount (₹ formatted)
  - Balance After
  - Transaction Date
  - Notes
- "Add Transaction" button for quick deposits/withdrawals

---

## 🏠 Dashboard (Home)

### 18. Dashboard (/Home/Index)
- Overview statistics displayed in card format:
  - **Members**: Total, Active, Inactive counts
  - **Staff**: Total, Active, Inactive counts
  - **Admins**: Total, Active, Inactive counts
  - **Accounts**: Total, Active, Inactive counts
- Transaction statistics:
  - Total Transactions count
  - Deposit count and total amount
  - Withdrawal count and total amount
- Account Type Distribution:
  - Savings, Current, Fixed Deposit, Loan account counts
- Quick navigation links to respective management sections
- Role-based visibility (Admin sees staff management link)

---

## 👨‍💼 Admin Panel (Admin-Only Features)

### 19. User Management (/Admin/Users)
- Displays all users (Staff and Admins) in a table
- Shows: Username, Email, Role, Status
- Actions available per user:
  - View Details
  - Toggle Status (Activate/Deactivate)
  - Change Role (Admin ↔ Staff)
  - Delete User
- Admin cannot modify their own account (self-protection)

### 20. User Details (/Admin/UserDetails/{id})
- Shows complete user information
- Displays: Username, Email, Role, Status, Created Date
- Action buttons for status toggle, role change, and deletion

### 21. Toggle User Status (/Admin/ToggleStatus/{id})
- Activates or deactivates a user account
- Deactivated users cannot log in
- Current admin cannot deactivate themselves

### 22. Change User Role (/Admin/ChangeRole/{id})
- Switches user role between Admin and Staff
- Current admin cannot demote themselves
- Role determines access to Admin panel features

### 23. Delete User (/Admin/DeleteUser/{id})
- Permanently removes user from system
- Current admin cannot delete themselves
- Confirmation required before deletion

### 24. Register New Staff (/Admin/Register)
- Admin-only feature to create new user accounts
- Form fields: Username, Email, Password, Confirm Password
- Validates:
  - Username uniqueness
  - Email uniqueness
  - Password requirements
- New users created with Staff role by default
- On success: Redirects to Users list

---

## 🔐 Authorization & Roles

### Role-Based Access Control
- **Admin**: Full access to all features including Admin Panel
- **Staff**: Access to Member, Account, Share, and Transaction management
- Both roles: Protected with `[Authorize]` attribute

### Route Protection
- `/Admin/*` routes: Admin role only
- `/AccountTransaction/*` routes: Admin and Staff roles
- Other management routes: Authenticated users

---
