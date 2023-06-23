INSERT INTO [dbo].[clients] ([name], [email], [phone], [address], [created_at])
VALUES
    ('John Doe', 'johndoe@example.com', '1234567890', '123 Main St', GETDATE()),
    ('Jane Smith', 'janesmith@example.com', '0987654321', '456 Elm St', GETDATE()),
    ('Mike Johnson', 'mikejohnson@example.com', '5555555555', '789 Oak St', GETDATE()),
    ('Emily Davis', 'emilydavis@example.com', '7777777777', '789 Maple Ave', GETDATE()),
    ('David Brown', 'davidbrown@example.com', '9876543210', '321 Pine St', GETDATE()),
    ('Sarah Wilson', 'sarahwilson@example.com', '8888888888', '456 Oak St', GETDATE());