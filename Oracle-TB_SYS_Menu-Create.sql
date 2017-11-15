-- Create table
create table TB_SYS_Menu
(
  menuid			varchar2(50),
  menutype			varchar2(50),
  menuname			varchar2(150),
  menulink			varchar2(150),
  menuicon			varchar2(50),
  menuiconpath		varchar2(150),
  menuisleaf		number,
  menuisvalid		number,
  menuishidden		number,
  menuno			number,
  menuremark		varchar2(150),
  menuparentid		varchar2(50),
  menuoperation		varchar2(50),
  menucreateby		varchar2(50),
  menucreatetime	date,
  menuupdateby		varchar2(50),
  menuupdatetime	date
)
tablespace USERS
  storage
  (
    initial 1M
    next 1M
    minextents 1
    pctincrease 0
  );
-- Add comments to the columns 
comment on column TB_SYS_Menu.menuid
  is 'GUID';
comment on column TB_SYS_Menu.menutype
  is '菜单类型:top,slide,tree';
comment on column TB_SYS_Menu.menuname
  is '菜单名称';
comment on column TB_SYS_Menu.menulink
  is '菜单地址';
comment on column TB_SYS_Menu.menuicon
  is '菜单图标';
comment on column TB_SYS_Menu.menuiconpath
  is '菜单图标地址';
comment on column TB_SYS_Menu.menuisleaf
  is '菜单是否子节点';
comment on column TB_SYS_Menu.menuisvalid
  is '菜单是否可用';
comment on column TB_SYS_Menu.menuishidden
  is '菜单是否隐藏';
comment on column TB_SYS_Menu.menuno
  is '菜单编号';
comment on column TB_SYS_Menu.menuremark
  is '菜单备注';
comment on column TB_SYS_Menu.menuparentid
  is '菜单父Id';
comment on column TB_SYS_Menu.menuoperation
  is '菜单操作';
comment on column TB_SYS_Menu.menucreateby
  is '菜单创建人';
comment on column TB_SYS_Menu.menucreatetime
  is '菜单创建时间';
comment on column TB_SYS_Menu.menuupdateby
  is '菜单更新人';
comment on column TB_SYS_Menu.menuupdatetime
  is '菜单更新时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_SYS_Menu
  add constraint PK_SysMenu primary key (MENUID);
