/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2013/11/26 星期二 22:20:39                      */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Business')
            and   type = 'U')
   drop table Business
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Customer')
            and   type = 'U')
   drop table Customer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FoodInfo')
            and   type = 'U')
   drop table FoodInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Orders')
            and   type = 'U')
   drop table Orders
go

/*==============================================================*/
/* Table: Business                                              */
/*==============================================================*/
create table Business (
   BusinessID           uniqueidentifier     not null,
   BusinessName         varchar(40)          not null,
   BusinessImage        image                null,
   BusinessGrade        float                null,
   BusinessAvePay       money                null,
   BusinessStyle        varchar(20)          null,
   BusinessAddress      varchar(200)         not null,
   BusinessPhone        varchar(40)          not null,
   BusinessDescribe     varchar(1000)        null,
   BusinessRemind       varchar(1000)        null,
   BusinessIsdeleted    bit                  not null,
   constraint PK_BUSINESS primary key nonclustered (BusinessID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商家信息',
   'user', @CurrentUser, 'table', 'Business'
go

/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table Customer (
   CustID               uniqueidentifier     not null,
   CustName             varchar(30)          not null,
   CustPassword         varchar(10)          not null,
   CustPhone            varchar(40)          not null,
   CustIsDeleted        bit                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户信息',
   'user', @CurrentUser, 'table', 'Customer'
go

/*==============================================================*/
/* Table: FoodInfo                                              */
/*==============================================================*/
create table FoodInfo (
   FoodID               uniqueidentifier     not null,
   FoodBusinessID       int                  not null,
   FoodName             varchar(20)          not null,
   FoodImage            image                null,
   FoodPopularIndex     int                  not null,
   FoodPrice            money                not null,
   FoodTaste            varchar(20)          not null,
   FoodIsdeleted        bit                  not null,
   constraint PK_FOODINFO primary key nonclustered (FoodID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '每道菜的详细信息',
   'user', @CurrentUser, 'table', 'FoodInfo'
go

/*==============================================================*/
/* Table: Orders                                                */
/*==============================================================*/
create table Orders (
   OrdersID             uniqueidentifier     not null,
   OrdersBusinessID     uniqueidentifier     not null,
   OrdersCustID         uniqueidentifier     not null,
   OrdersFoodInfoID     uniqueidentifier     not null,
   OrdersData           datetime             not null,
   OrdersNum            int                  not null,
   constraint PK_ORDERS primary key nonclustered (OrdersID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户下单的信息',
   'user', @CurrentUser, 'table', 'Orders'
go

