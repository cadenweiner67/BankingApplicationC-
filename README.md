# CptS321-in-class-exercises​

Caden Weiner

11620192

Final Exam

UserNames And Passwords


1)
Features
Implemented functionality to create accounts of different types using an account factory (saving, checking and loan). Implemented loan payments, deposits into accounts, withdrawals and transfers. Withdrawals, transfers, deposits and loan payments are all implemented on users accounts based on their account numbers. Employees can create new accounts for users. Accounts can store and display their past 10 operations. Accounts can show their account info. Withdrawals and deposits must occur to accounts that belong to a user. A transfer must be first withdrawed from the account that request it as we do not want people to be able to transfer money from accounts that are not theirs into their own account. Employees can create new accounts for existing users. Accounts and users are stored and loaded using xml. Transfers can be undone within 10 seconds of their execution. This is done by making use of mutex locks and threading. This information needs to be protected this way for any multithreading application to prevent race conditions. The login library is able to verify users who attempt to log in. When users and accounts are loaded in. The users list of accounts that are associated with it is updated. 


2)
Accounts 

Client : 
Username -> Caden Weiner
Password -> 123
Username -> Lycoris
Password -> 123

Employee : 
Username -> Brock
Password -> Rock

Link to Video : https://youtu.be/RpVGaf8PT5A

#   B a n k i n g A p p l i c a t i o n C -  
 