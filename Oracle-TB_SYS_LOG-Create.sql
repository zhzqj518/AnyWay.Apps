-- Create table
create table TB_SYS_LOG
(
  id         VARCHAR2(50) not null,
  operator   VARCHAR2(50),
  operatorip VARCHAR2(50),
  message    VARCHAR2(500),
  result     VARCHAR2(20),
  type       VARCHAR2(20),
  module     VARCHAR2(20),
  createtime DATE
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Add comments to the columns 
comment on column TB_SYS_LOG.id
  is 'GUID';
comment on column TB_SYS_LOG.operator
  is '操作人';
comment on column TB_SYS_LOG.operatorip
  is '操作人IP地址'
comment on column TB_SYS_LOG.message
  is '操作信息';
comment on column TB_SYS_LOG.result
  is '结果';
comment on column TB_SYS_LOG.type
  is '操作类型';
comment on column TB_SYS_LOG.module
  is '操作模块';
comment on column TB_SYS_LOG.createtime
  is '操作时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table TB_SYS_LOG
  add constraint PK_SYSLOG primary key (ID)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
