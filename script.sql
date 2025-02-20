
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [fk_id_order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [fk_id_order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [fk_id_product_2] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [fk_id_product_2]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [fk_id_status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[StatusOrder] ([StatusId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [fk_id_status]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [fk_id_user_2] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [fk_id_user_2]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [fk_id_category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [fk_id_category]
GO
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [fk_id_role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [fk_id_role]
GO
USE [master]
GO
ALTER DATABASE [OnlineShop] SET  READ_WRITE 
GO
