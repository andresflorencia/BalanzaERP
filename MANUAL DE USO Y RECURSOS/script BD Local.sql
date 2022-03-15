CREATE TABLE "public"."detallemarqueta" (
  "transaccionid" int4,
  "linea" int4,
  "productoid" int4,
  "numerolote" varchar(50) COLLATE "pg_catalog"."default",
  "codigomarqueta" varchar(100) COLLATE "pg_catalog"."default",
  "cantidad" numeric(10,2),
  "fechavencimiento" varchar(20) COLLATE "pg_catalog"."default",
  "fecharegistro" timestamptz(6) DEFAULT now(),
  "nummarmita" int4 DEFAULT 0, 
  "origenpesado" varchar(1) DEFAULT 'B'
)
;

ALTER TABLE "public"."detallemarqueta" OWNER TO "postgres";

CREATE TABLE "public"."detalletransaccion" (
  "transaccionid" int4,
  "linea" int4,
  "productoid" int4,
  "cantidad" numeric(10,2),
  "total" numeric(10,2) DEFAULT 0,
  "precio" numeric(10,2) DEFAULT 0,
  "numerolote" varchar(50) COLLATE "pg_catalog"."default",
  "fechavencimiento" varchar(20) COLLATE "pg_catalog"."default",
  "stock" numeric(10,2) DEFAULT 0,
  "preciocosto" numeric(10,2) DEFAULT 0,
  "precioreferencia" numeric(10,2) DEFAULT 0,
  "valoriva" numeric(10,2) DEFAULT 0,
  "valorice" numeric(10,2) DEFAULT 0,
  "descuento" numeric(10,2) DEFAULT 0,
  "marquetas" numeric(10,2) DEFAULT 0,
  "codigoproducto" varchar(50) COLLATE "pg_catalog"."default" DEFAULT ''::character varying
)
;

ALTER TABLE "public"."detalletransaccion" OWNER TO "postgres";

CREATE TABLE "public"."establecimiento" (
  "idestablecimiento" int4 NOT NULL,
  "codigoestablecimiento" varchar(25) COLLATE "pg_catalog"."default",
  "rucempresa" varchar(13) COLLATE "pg_catalog"."default",
  "nombreestablecimiento" varchar(100) COLLATE "pg_catalog"."default",
  "direccion" varchar(255) COLLATE "pg_catalog"."default",
  "parroquiaid" int4,
  "tipo" varchar(4) COLLATE "pg_catalog"."default",
  "nombrecomercial" varchar(100) COLLATE "pg_catalog"."default",
  "codigoestablecimientosri" varchar(3) COLLATE "pg_catalog"."default",
  "nummarmita" int4 DEFAULT 0,
  CONSTRAINT "establecimiento_pkey" PRIMARY KEY ("idestablecimiento")
)
;

ALTER TABLE "public"."establecimiento" OWNER TO "postgres";

CREATE TABLE "public"."producto" (
  "idproducto" int4 NOT NULL,
  "codigoproducto" varchar(100) COLLATE "pg_catalog"."default",
  "nombreproducto" varchar(250) COLLATE "pg_catalog"."default",
  "pvp" numeric(10,2),
  "unidadid" int4,
  "unidadesporcaja" int4,
  "iva" int4,
  "ice" int4,
  "factorconversion" numeric(10,2),
  "stock" numeric(10,2),
  "porcentajeiva" numeric(10,2),
  CONSTRAINT "producto_pkey" PRIMARY KEY ("idproducto")
)
;

ALTER TABLE "public"."producto" OWNER TO "postgres";

CREATE TABLE "public"."secuencial" (
  "idsecuencial" int4 NOT NULL GENERATED ALWAYS AS IDENTITY (
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
),
  "periodo" varchar(20) COLLATE "pg_catalog"."default",
  "tipodocumento" varchar(10) COLLATE "pg_catalog"."default",
  "establecimiento" varchar(10) COLLATE "pg_catalog"."default",
  "puntoemision" varchar(10) COLLATE "pg_catalog"."default",
  "valor" varchar(10) COLLATE "pg_catalog"."default",
  "estado" bool DEFAULT true,
  CONSTRAINT "secuencial_pkey" PRIMARY KEY ("idsecuencial")
)
;

ALTER TABLE "public"."secuencial" OWNER TO "postgres";

CREATE TABLE "public"."transaccion" (
  "idtransaccion" int4 NOT NULL GENERATED ALWAYS AS IDENTITY (
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
),
  "establecimientoid" int4,
  "personaid" int4,
  "usuarioid" int4,
  "codigosistema" int4 DEFAULT 0,
  "codigotransaccion" varchar(50) COLLATE "pg_catalog"."default",
  "tipotransaccion" varchar(2) COLLATE "pg_catalog"."default",
  "fecharegistro" timestamptz(6) DEFAULT now(),
  "fechadocumento" varchar(20) COLLATE "pg_catalog"."default",
  "observacion" text COLLATE "pg_catalog"."default",
  "subtotal" numeric(10,2) DEFAULT 0,
  "subtotaliva" numeric(10,2) DEFAULT 0,
  "descuento" numeric(10,2) DEFAULT 0,
  "porcentajeiva" numeric(10,2) DEFAULT 0,
  "total" numeric(10,2) DEFAULT 0,
  "estado" int4 DEFAULT 1,
  "secuencial" int4 DEFAULT 0,
  "fechaproduccion" varchar(20) COLLATE "pg_catalog"."default",
  CONSTRAINT "transaccion_pkey" PRIMARY KEY ("idtransaccion")
)
;

ALTER TABLE "public"."transaccion" OWNER TO "postgres";

CREATE TABLE "public"."usuario" (
  "idusuario" int4 NOT NULL,
  "razonsocial" varchar(128) COLLATE "pg_catalog"."default",
  "usuario" varchar(50) COLLATE "pg_catalog"."default",
  "clave" varchar(128) COLLATE "pg_catalog"."default",
  "perfil" int4,
  "establecimientoid" int4,
  "parroquiaid" int4,
  "nombreperfil" varchar(50) COLLATE "pg_catalog"."default",
  "nip" varchar(20) COLLATE "pg_catalog"."default",
  "puntoemisionid" int4 DEFAULT 0,
  CONSTRAINT "usuario_pkey" PRIMARY KEY ("idusuario")
)
;

ALTER TABLE "public"."usuario" OWNER TO "postgres";


CREATE OR REPLACE FUNCTION "public"."func_num_transaccion"("fecha" varchar=''::character varying)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN
  SELECT COUNT(*) INTO retorno FROM transaccion WHERE date(fechaproduccion) = date(fecha);
  return retorno;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."func_num_transaccion"("fecha" varchar) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."func_secuencial"("tipoaccion" varchar=''::character varying, "aperiodo" varchar=''::character varying, "atipodocumento" varchar=''::character varying, "aestablecimiento" varchar=''::character varying, "apuntoemision" varchar=''::character varying, "evalor" int4=0)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN 
  --BEGIN TRANSACTION Trans 
  IF tipoaccion = 'ULTIMO' THEN
    IF (SELECT COUNT(*) FROM secuencial WHERE periodo = aperiodo AND
            tipodocumento = atipodocumento AND establecimiento = aestablecimiento AND
            puntoemision = apuntoemision) = 0 THEN 
      INSERT INTO secuencial (periodo, tipodocumento, establecimiento, puntoemision, valor)
      VALUES(aperiodo, atipodocumento, aestablecimiento, apuntoemision, 0);
      RETURN 0;
    ELSE
      SELECT valor INTO retorno FROM secuencial WHERE periodo = aperiodo AND
            tipodocumento = atipodocumento AND establecimiento = aestablecimiento AND
            puntoemision = apuntoemision;
      RETURN retorno;
    END IF;
  ELSIF tipoaccion = 'UPDATE' THEN
    UPDATE secuencial SET valor = evalor WHERE periodo = aperiodo AND
            tipodocumento = atipodocumento AND establecimiento = aestablecimiento AND
            puntoemision = apuntoemision;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."func_secuencial"("tipoaccion" varchar, "aperiodo" varchar, "atipodocumento" varchar, "aestablecimiento" varchar, "apuntoemision" varchar, "evalor" int4) OWNER TO "postgres";


CREATE OR REPLACE FUNCTION "public"."sp_bus_detallemarqueta"("eidtransaccion" int4=0)
  RETURNS SETOF "public"."detallemarqueta" AS $BODY$
declare REG RECORD;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  FOR REG IN
    SELECT * FROM detallemarqueta where transaccionid = eidtransaccion order by linea LOOP
    return next REG;
  END LOOP;
  return;
  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_bus_detallemarqueta"("eidtransaccion" int4) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_bus_detalletransaccion"("eidtransaccion" int4=0)
  RETURNS SETOF "public"."detalletransaccion" AS $BODY$
declare REG RECORD;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  FOR REG IN
    SELECT * FROM detalletransaccion where transaccionid = eidtransaccion order by linea LOOP
    return next REG;
  END LOOP;
  return;
  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_bus_detalletransaccion"("eidtransaccion" int4) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_bus_establecimiento"("tipoaccion" varchar, "codigo" int4=NULL::integer)
  RETURNS SETOF "public"."establecimiento" AS $BODY$
declare REG RECORD;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF tipoAccion = 'LISTA' THEN 
    FOR REG IN
      SELECT * FROM establecimiento LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'FILL' THEN
    FOR REG IN
      SELECT * FROM establecimiento WHERE idestablecimiento = codigo LOOP
      return next REG;
    END LOOP;
    return;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_bus_establecimiento"("tipoaccion" varchar, "codigo" int4) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_bus_producto"("tipoaccion" varchar, "codigo" int4=NULL::integer, "filtro" varchar=''::character varying)
  RETURNS SETOF "public"."producto" AS $BODY$
declare REG RECORD;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF tipoAccion = 'LISTA' THEN 
    FOR REG IN
      SELECT * FROM producto LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'FILTER' THEN
    FOR REG IN
      SELECT * FROM producto WHERE lower(nombreproducto || codigoproducto) like '%' || lower(filtro) ||'%' LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'FILL' THEN
    FOR REG IN
      SELECT * FROM producto WHERE idproducto = codigo LOOP
      return next REG;
    END LOOP;
    return;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_bus_producto"("tipoaccion" varchar, "codigo" int4, "filtro" varchar) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_bus_transaccion"("tipoaccion" varchar, "eidtransaccion" int4=0, "acodigotransaccion" varchar=''::character varying)
  RETURNS SETOF "public"."transaccion" AS $BODY$
declare REG RECORD;
DECLARE retorno INTEGER;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF tipoAccion = 'LISTA' THEN 
    FOR REG IN
      SELECT * FROM transaccion where estado > 0 order by idtransaccion desc LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'POR_SINCRONIZAR' THEN
    FOR REG IN
      SELECT * FROM transaccion WHERE estado > 0 AND codigosistema = 0 ORDER BY idtransaccion LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'BORRADOR' THEN
    FOR REG IN
      SELECT * FROM transaccion WHERE estado = 2 AND codigosistema = 0 ORDER BY idtransaccion LIMIT 1 LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'POR_ID' THEN
    FOR REG IN
      SELECT * FROM transaccion WHERE idtransaccion = eidtransaccion LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'POR_CODIGO' THEN
    FOR REG IN
      SELECT tr.* FROM transaccion tr 
      JOIN detalletransaccion dt on dt.transaccionid = tr.idtransaccion
      WHERE tr.codigotransaccion = acodigotransaccion or dt.numerolote = acodigotransaccion LOOP
      return next REG;
    END LOOP;
    return;
  ELSIF tipoaccion = 'NUM_FECHA' THEN
    SELECT COUNT(*) INTO REG FROM transaccion WHERE date(fecharegistro) = date(acodigotransaccion);
    return;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_bus_transaccion"("tipoaccion" varchar, "eidtransaccion" int4, "acodigotransaccion" varchar) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_grab_detallemarqueta"("tipoaccion" varchar=''::character varying, "etransaccionid" int4=0, "elinea" int4=0, "eproductoid" int4=0, "anumerolote" varchar=''::character varying, "acodigomarqueta" varchar=''::character varying, "dcantidad" float8=0, "afechavencimiento" varchar=''::character varying, "enummarmita" float8=0, "aorigenpesado" varchar=''::character varying)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN 
  --BEGIN TRANSACTION Trans 
  IF tipoaccion = 'NUEVO' THEN
    
    INSERT INTO detallemarqueta 
      (transaccionid, linea, productoid, numerolote, codigomarqueta, cantidad, fechavencimiento, nummarmita, origenpesado)
    VALUES(etransaccionid, elinea, eproductoid, anumerolote, acodigomarqueta, dcantidad, afechavencimiento, enummarmita, aorigenpesado);
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSIF tipoaccion = 'ELIMINAR' THEN
    DELETE FROM detallemarqueta
    WHERE transaccionid = etransaccionid;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSIF tipoaccion = 'ELIMINAR_LINEA' THEN
    DELETE FROM detallemarqueta
    WHERE transaccionid = etransaccionid AND linea = elinea;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_detallemarqueta"("tipoaccion" varchar, "etransaccionid" int4, "elinea" int4, "eproductoid" int4, "anumerolote" varchar, "acodigomarqueta" varchar, "dcantidad" float8, "afechavencimiento" varchar, "enummarmita" float8, "aorigenpesado" varchar) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_grab_detalletransaccion"("tipoaccion" varchar=''::character varying, "etransaccionid" int4=0, "elinea" int4=0, "eproductoid" int4=0, "dcantidad" float8=0, "dtotal" float8=0, "dprecio" float8=0, "anumerolote" varchar=''::character varying, "afechavencimiento" varchar=''::character varying, "dstock" float8=0, "dpreciocosto" float8=0, "dprecioreferencia" float8=0, "dmarquetas" float8=0, "acodigoproducto" varchar=''::character varying)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN 
  --BEGIN TRANSACTION Trans 
  IF tipoaccion = 'NUEVO' THEN
    
    INSERT INTO detalletransaccion (transaccionid, linea, productoid, cantidad, total,
        precio, numerolote, fechavencimiento, stock, preciocosto, precioreferencia, 
        marquetas, codigoproducto)
    VALUES(etransaccionid, elinea, eproductoid, dcantidad, dtotal,
        dprecio, anumerolote, afechavencimiento, dstock, dpreciocosto, dprecioreferencia,
         dmarquetas, acodigoproducto);
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSIF tipoaccion = 'ELIMINAR' THEN
    DELETE FROM detalletransaccion 
    WHERE transaccionid = etransaccionid;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_detalletransaccion"("tipoaccion" varchar, "etransaccionid" int4, "elinea" int4, "eproductoid" int4, "dcantidad" float8, "dtotal" float8, "dprecio" float8, "anumerolote" varchar, "afechavencimiento" varchar, "dstock" float8, "dpreciocosto" float8, "dprecioreferencia" float8, "dmarquetas" float8, "acodigoproducto" varchar) OWNER TO "postgres";


CREATE OR REPLACE FUNCTION "public"."sp_grab_establecimiento"("eidestablecimiento" int4=0, "acodigoestablecimiento" varchar=''::character varying, "arucempresa" varchar=''::character varying, "anombreestablecimiento" varchar=''::character varying, "adireccion" varchar=''::character varying, "atipo" varchar=''::character varying, "anombrecomercial" varchar=''::character varying, "acodigoestablecimientosri" varchar=''::character varying, "enummarmita" int4=0)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF (SELECT COUNT(*) FROM establecimiento WHERE idestablecimiento = eidestablecimiento)>0 THEN 
    UPDATE establecimiento SET
      idestablecimiento = eidestablecimiento,
      codigoestablecimiento = acodigoestablecimiento, 
      rucempresa = arucempresa,
      nombreestablecimiento = anombreestablecimiento,
      direccion = adireccion,
      tipo = atipo, 
      nombrecomercial = anombrecomercial,
      codigoestablecimientosri = acodigoestablecimientosri,
      nummarmita = enummarmita
    WHERE idestablecimiento = eidestablecimiento; 
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSE 
    INSERT INTO establecimiento (idestablecimiento,codigoestablecimiento, rucempresa,
      nombreestablecimiento,direccion,tipo,nombrecomercial,codigoestablecimientosri, nummarmita)
    values(eidestablecimiento, acodigoestablecimiento, arucempresa,
      anombreestablecimiento, adireccion, atipo, anombrecomercial, acodigoestablecimientosri, enummarmita);
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_establecimiento"("eidestablecimiento" int4, "acodigoestablecimiento" varchar, "arucempresa" varchar, "anombreestablecimiento" varchar, "adireccion" varchar, "atipo" varchar, "anombrecomercial" varchar, "acodigoestablecimientosri" varchar, "enummarmita" int4) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_grab_producto"("eidproducto" int4=0, "acodigoproducto" varchar=''::character varying, "anombreproducto" varchar=''::character varying, "dpvp" float8=0, "eunidadid" int4=0, "eunidadesporcaja" int4=0, "eiva" int4=0, "eice" int4=0, "dfactorconversion" float8=0, "dstock" float8=0, "dporcentajeiva" float8=0)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF (SELECT COUNT(*) FROM producto WHERE idproducto = eidproducto)>0 THEN 
    UPDATE producto SET
      idproducto = eidproducto,
      codigoproducto = acodigoproducto,
      nombreproducto = anombreproducto,
      pvp = dpvp,
      unidadid = eunidadid,
      unidadesporcaja = eunidadesporcaja, 
      iva = eiva,
      ice = eice,
      factorconversion = dfactorconversion,
      stock = dstock,
      porcentajeiva = dporcentajeiva
    WHERE idproducto = eidproducto; 
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSE 
    INSERT INTO producto (idproducto, codigoproducto, nombreproducto, pvp, unidadid,
      unidadesporcaja, iva, ice, factorconversion, stock, porcentajeiva)
    values(eidproducto,acodigoproducto,anombreproducto,dpvp,eunidadid,
      eunidadesporcaja, eiva,eice,dfactorconversion,dstock,dporcentajeiva);
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_producto"("eidproducto" int4, "acodigoproducto" varchar, "anombreproducto" varchar, "dpvp" float8, "eunidadid" int4, "eunidadesporcaja" int4, "eiva" int4, "eice" int4, "dfactorconversion" float8, "dstock" float8, "dporcentajeiva" float8) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_grab_transaccion"("tipoaccion" varchar=''::character varying, "eidtransaccion" int4=0, "eestablecimientoid" int4=0, "epersonaid" int4=0, "eusuarioid" int4=0, "ecodigosistema" int4=0, "acodigotransaccion" varchar=''::character varying, "atipotransaccion" varchar=''::character varying, "afechadocumento" varchar=''::character varying, "aobservacion" varchar=''::character varying, "eestado" int4=1, "esecuencial" int4=0, "afechaproduccion" varchar=''::character varying)
  RETURNS "pg_catalog"."int4" AS $BODY$
DECLARE retorno INTEGER;
BEGIN 
  --BEGIN TRANSACTION Trans 
  IF tipoaccion = 'NUEVO' THEN
    
    INSERT INTO transaccion (establecimientoid, personaid, usuarioid,
      codigosistema, codigotransaccion, tipotransaccion, fechadocumento,
      observacion, estado, secuencial, fechaproduccion)
    VALUES(eestablecimientoid, epersonaid, eusuarioid,
      ecodigosistema, acodigotransaccion, atipotransaccion, afechadocumento,
      aobservacion, eestado, esecuencial, afechaproduccion) RETURNING idtransaccion INTO retorno;
      RETURN retorno;
  ELSIF tipoaccion = 'UPDATE' THEN
    UPDATE transaccion 
    SET establecimientoid = eestablecimientoid, 
      personaid=epersonaid, 
      usuarioid = eusuarioid,
      codigosistema = ecodigosistema,  
      tipotransaccion = atipotransaccion, 
      fechadocumento = afechadocumento,
      observacion = aobservacion, 
      estado = eestado, 
      fechaproduccion = afechaproduccion
    WHERE idtransaccion = eidtransaccion;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  ELSIF tipoaccion = 'ESTADO' THEN
    UPDATE transaccion 
    SET codigosistema = ecodigosistema, 
      estado = ecodigosistema 
    WHERE idtransaccion = eidtransaccion;
    GET DIAGNOSTICS retorno = ROW_COUNT;
    RETURN retorno;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_transaccion"("tipoaccion" varchar, "eidtransaccion" int4, "eestablecimientoid" int4, "epersonaid" int4, "eusuarioid" int4, "ecodigosistema" int4, "acodigotransaccion" varchar, "atipotransaccion" varchar, "afechadocumento" varchar, "aobservacion" varchar, "eestado" int4, "esecuencial" int4, "afechaproduccion" varchar) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_grab_usuario"("eidusuario" int4=0, "ausuario" varchar=''::character varying, "aclave" varchar=''::character varying, "arazonsocial" varchar=''::character varying, "anip" varchar=''::character varying, "eperfil" int4=0, "eestablecimientoid" int4=0, "eparroquiaid" int4=0, "anombreperfil" varchar=''::character varying, "epuntoemisionid" int4=0)
  RETURNS "pg_catalog"."int4" AS $BODY$
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF (SELECT COUNT(*) FROM usuario WHERE usuario.usuario = ausuario)>0 THEN 
    UPDATE usuario SET
      idusuario = eidusuario,
      razonsocial = arazonsocial,
      clave = MD5(aclave),
      perfil = eperfil,
      establecimientoid = eestablecimientoid,
      parroquiaid = eparroquiaid,
      nombreperfil = anombreperfil,
      nip = anip,
      puntoemisionid = epuntoemisionid
    WHERE usuario = ausuario;   
    RETURN eidusuario;
  ELSE 
    INSERT INTO usuario values(eidusuario, arazonsocial, ausuario, 
               MD5(aclave), eperfil, eestablecimientoid,
              eparroquiaid, anombreperfil, anip, epuntoemisionid);              
    RETURN eidusuario;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;

ALTER FUNCTION "public"."sp_grab_usuario"("eidusuario" int4, "ausuario" varchar, "aclave" varchar, "arazonsocial" varchar, "anip" varchar, "eperfil" int4, "eestablecimientoid" int4, "eparroquiaid" int4, "anombreperfil" varchar, "epuntoemisionid" int4) OWNER TO "postgres";

CREATE OR REPLACE FUNCTION "public"."sp_login"("tipoaccion" varchar, "codigo" varchar=NULL::character varying, "ausuario" varchar=''::character varying, "aclave" varchar=''::character varying)
  RETURNS SETOF "public"."usuario" AS $BODY$
declare REG RECORD;
BEGIN
  --BEGIN TRANSACTION Trans select * from rest_mesa
  IF tipoAccion = 'LOGIN' THEN 
    FOR REG IN
      SELECT * FROM usuario WHERE usuario.usuario = ausuario AND usuario.clave = MD5(aclave) LIMIT 1 LOOP
      return next REG;
    END LOOP;
    return;
  END IF;

  --COMMIT TRANSACTION Trans
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;

ALTER FUNCTION "public"."sp_login"("tipoaccion" varchar, "codigo" varchar, "ausuario" varchar, "aclave" varchar) OWNER TO "postgres";