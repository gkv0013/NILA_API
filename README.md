# gkv0013
-- Table: public.users

-- DROP TABLE IF EXISTS public.users;

CREATE TABLE IF NOT EXISTS public.users
(
    id integer NOT NULL DEFAULT nextval('users_id_seq'::regclass),
    telegramid bigint NOT NULL,
    username character varying(255) COLLATE pg_catalog."default",
    firstname character varying(255) COLLATE pg_catalog."default" NOT NULL,
    lastname character varying(255) COLLATE pg_catalog."default" NOT NULL,
    created_at timestamp without time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    last_login timestamp without time zone,
    profit_per_tap numeric(10,2) NOT NULL,
    profit_per_hour numeric(10,2) NOT NULL,
    button_press_count integer NOT NULL DEFAULT 0,
    total_coins bigint NOT NULL DEFAULT 0,
    friends_invited integer NOT NULL DEFAULT 0,
    referral_bonus numeric(10,2) NOT NULL DEFAULT 0.00,
    daily_login_streak integer NOT NULL DEFAULT 0,
    CONSTRAINT users_pkey PRIMARY KEY (id),
    CONSTRAINT users_telegramid_key UNIQUE (telegramid)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.users
    OWNER to postgres;



    -- FUNCTION: public.insert_user(bigint, character varying, character varying, numeric, numeric, character varying, timestamp without time zone, integer, bigint, integer, numeric, integer)

-- DROP FUNCTION IF EXISTS public.insert_user(bigint, character varying, character varying, numeric, numeric, character varying, timestamp without time zone, integer, bigint, integer, numeric, integer);

CREATE OR REPLACE FUNCTION public.insert_user(
	p_telegramid bigint,
	p_firstname character varying,
	p_lastname character varying,
	p_profit_per_tap numeric,
	p_profit_per_hour numeric,
	p_username character varying DEFAULT NULL::character varying,
	p_last_login timestamp without time zone DEFAULT NULL::timestamp without time zone,
	p_button_press_count integer DEFAULT 0,
	p_total_coins bigint DEFAULT 0,
	p_friends_invited integer DEFAULT 0,
	p_referral_bonus numeric DEFAULT 0.00,
	p_daily_login_streak integer DEFAULT 0)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_id INTEGER;
BEGIN
    INSERT INTO users (
        telegramid,
        username,
        firstname,
        lastname,
        created_at,
        last_login,
        profit_per_tap,
        profit_per_hour,
        button_press_count,
        total_coins,
        friends_invited,
        referral_bonus,
        daily_login_streak
    ) VALUES (
        p_telegramid,
        p_username,
        p_firstname,
        p_lastname,
        CURRENT_TIMESTAMP,
        p_last_login,
        p_profit_per_tap,
        p_profit_per_hour,
        p_button_press_count,
        p_total_coins,
        p_friends_invited,
        p_referral_bonus,
        p_daily_login_streak
    ) RETURNING id INTO v_id;

    RETURN v_id;  -- Return the auto-generated ID
END;
$BODY$;

ALTER FUNCTION public.insert_user(bigint, character varying, character varying, numeric, numeric, character varying, timestamp without time zone, integer, bigint, integer, numeric, integer)
    OWNER TO postgres;
-- FUNCTION: public.update_user(integer, bigint, timestamp without time zone, bigint)

-- DROP FUNCTION IF EXISTS public.update_user(integer, bigint, timestamp without time zone, bigint);

CREATE OR REPLACE FUNCTION public.update_user(
	p_mode integer,
	p_telegramid bigint,
	p_last_login timestamp without time zone DEFAULT NULL::timestamp without time zone,
	p_total_coins bigint DEFAULT NULL::bigint)
    RETURNS boolean
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_exists BOOLEAN;
BEGIN
    CASE p_mode
        WHEN 0 THEN
            -- Mode 0: Update last_login based on telegramid
            UPDATE users
            SET last_login = COALESCE(p_last_login, CURRENT_TIMESTAMP)
            WHERE telegramid = p_telegramid;
            RETURN TRUE;

        WHEN 1 THEN
            -- Mode 1: Update total_coins based on telegramid
            UPDATE users
            SET total_coins = COALESCE(p_total_coins, total_coins)
            WHERE telegramid = p_telegramid;
            RETURN TRUE;

        WHEN 2 THEN
            -- Mode 2: Update both last_login and total_coins based on telegramid
            UPDATE users
            SET last_login = COALESCE(p_last_login, CURRENT_TIMESTAMP),
                total_coins = COALESCE(p_total_coins, total_coins)
            WHERE telegramid = p_telegramid;
            RETURN TRUE;
			
        WHEN 3 THEN
            -- Mode 3: Check if telegramid exists in the users table
            SELECT EXISTS (
                SELECT 1 FROM users WHERE telegramid = p_telegramid
            ) INTO v_exists;
            RETURN v_exists;
        ELSE
            -- Invalid mode
            RAISE EXCEPTION 'Invalid mode: %', p_mode;
            RETURN FALSE;
    END CASE;
END;
$BODY$;

ALTER FUNCTION public.update_user(integer, bigint, timestamp without time zone, bigint)
    OWNER TO postgres;
-- FUNCTION: public.user_login(integer, bigint, character varying, character varying, character varying, numeric, numeric, bigint, integer, numeric, integer, timestamp without time zone, refcursor)

-- DROP FUNCTION IF EXISTS public.user_login(integer, bigint, character varying, character varying, character varying, numeric, numeric, bigint, integer, numeric, integer, timestamp without time zone, refcursor);

CREATE OR REPLACE FUNCTION public.user_login(
	p_mode integer,
	p_telegramid bigint,
	p_username character varying,
	p_firstname character varying,
	p_lastname character varying,
	p_profit_per_tap numeric,
	p_profit_per_hour numeric,
	p_total_coins bigint,
	p_friends_invited integer,
	p_referral_bonus numeric,
	p_daily_login_streak integer,
	p_lastlogin timestamp without time zone,
	ref refcursor)
    RETURNS refcursor
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    v_user RECORD;
BEGIN
    -- Check if user exists by telegramid
    SELECT * INTO v_user
    FROM users
    WHERE telegramid = p_telegramid;

    -- Mode 0: Insert new user if not found
    IF p_mode = 0 THEN
        IF NOT FOUND THEN
            INSERT INTO users (telegramid, username, firstname, lastname, created_at, last_login, profit_per_tap, profit_per_hour, total_coins, friends_invited, referral_bonus, daily_login_streak)
            VALUES (p_telegramid, p_username, p_firstname, p_lastname, CURRENT_TIMESTAMP, p_lastLogin, p_profit_per_tap, p_profit_per_hour, p_total_coins, p_friends_invited, p_referral_bonus, p_daily_login_streak)
            RETURNING * INTO v_user;
           ELSE
            -- If user is found, update the last_login
            UPDATE users
            SET last_login = p_lastLogin
            WHERE telegramid = p_telegramid;
        END IF;

        -- Open the refcursor to return the user information
        OPEN ref FOR SELECT * FROM users WHERE telegramid = p_telegramid;

        -- Return the refcursor
        RETURN ref;

    -- Mode 1: Update profit_per_tap, profit_per_hour, total_coins, and last_login if user exists
    ELSIF p_mode = 1 THEN
        IF FOUND THEN
            UPDATE users
            SET profit_per_tap = p_profit_per_tap,
                profit_per_hour = p_profit_per_hour,
                total_coins = p_total_coins,
                last_login = p_lastLogin
            WHERE telegramid = p_telegramid;
        ELSE
            RAISE EXCEPTION 'User with telegramid % not found.', p_telegramid;
        END IF;
    ELSE
        RAISE EXCEPTION 'Invalid mode value. Mode must be 0 or 1.';
    END IF;

    -- In mode 1, do not return the refcursor
    RETURN null;
END;
$BODY$;

ALTER FUNCTION public.user_login(integer, bigint, character varying, character varying, character varying, numeric, numeric, bigint, integer, numeric, integer, timestamp without time zone, refcursor)
    OWNER TO postgres;
