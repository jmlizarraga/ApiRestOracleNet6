/*
drop trigger al_insertar_categoria;
drop sequence categoria_seq;
drop table categoria;

commit;
*/

create table categoria(
    id int not null,
    nombre varchar(50),
	constraint pk_categoria 
        primary key (id)
);

-- Secuencia para la categoria
create sequence categoria_seq start with 1 increment by 1 nocache nocycle;

-- Crear trigger para la categoria
create or replace trigger al_insertar_categoria before insert on categoria for each row
begin
    select categoria_seq.nextval into :new.id from dual;
end;


insert into categoria(nombre) values ('Articulos de Aseo');
insert into categoria(nombre) values ('Aparatos Electronicos');
commit;


select * from categoria;












