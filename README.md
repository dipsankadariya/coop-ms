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

I'm actively working on this  , readme will be updated timely with more features added..this is the flow of the application for now.
