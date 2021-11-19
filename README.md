# DeliverySystem
This project created for GlueHome - Platform Technical Task

For this project i used .NET Core 5.0 and Entity Framework Code First approach to store data.
I created logging mechanism for all endpoint actions. You can reach the log file under project directory /log.txt
I have authentication system which provided with JWT
Entegrated with swagger to read endpoints and their descriptions easily

Models
------------
User - IAccount,
Partner -IAccount

These models are using for logged in accounts. I created an interface to put common properties in it because of the SOLID. Other special properties store in related classes.

Product,
Order,
OrderItems

Product class has some properties about ordered item. It has unit price and detailed description of it.
All orders have order items. Because in one order i can put more than one product. Because of the normalization i created OrderItems table to linked Order and Product.

Delivery,
DeliveryAction

Delivery is my main class. It has order info and specific properties about the delivery.
I wanted to log all status changes to track who change the action and when. So i created delivery action table

Controllers
---------------
AccountController - Register and Login actions,
DeliveryController - All CRUD operations

Note : I wrote detailed explanation for each services and their methods. You can reach them in summary section
