/*------------------------------------------------------------
*        Script SQLSERVER 
------------------------------------------------------------*/


/*------------------------------------------------------------
-- Table: Types
------------------------------------------------------------*/
CREATE TABLE Types(
	id_type     INT IDENTITY (1,1) NOT NULL ,
	type_name   VARCHAR (50) NOT NULL  ,
	CONSTRAINT PK_Types PRIMARY KEY (id_type)
);


/*------------------------------------------------------------
-- Table: Categorie
------------------------------------------------------------*/
CREATE TABLE Categories(
	id_cat    INT IDENTITY (1,1) NOT NULL ,
	nom_cat   VARCHAR (80) NOT NULL  ,
	CONSTRAINT PK_Categorie PRIMARY KEY (id_cat)
);


/*------------------------------------------------------------
-- Table: Habitat
------------------------------------------------------------*/
CREATE TABLE Habitats(
	id_hab     INT IDENTITY (1,1) NOT NULL ,
	nom_hab    VARCHAR (50) NOT NULL ,
	desc_hab   TEXT  NOT NULL  ,
	CONSTRAINT PK_Habitat PRIMARY KEY (id_hab)
);


/*------------------------------------------------------------
-- Table: Modificateur
------------------------------------------------------------*/
CREATE TABLE Modificateurs(
	id_modif     INT IDENTITY (1,1) NOT NULL ,
	nom_modif    VARCHAR (250) NOT NULL ,
	abbr_modif   VARCHAR (250) NOT NULL ,
	val_modif    DECIMAL  NOT NULL  ,
	CONSTRAINT PK_Modificateur PRIMARY KEY (id_modif)
);


/*------------------------------------------------------------
-- Table: Date
------------------------------------------------------------*/
CREATE TABLE Dates(
	jjmmaaa   DATETIME NOT NULL  ,
	CONSTRAINT PK_Date PRIMARY KEY (jjmmaaa)
);


/*------------------------------------------------------------
-- Table: User
------------------------------------------------------------*/
CREATE TABLE Users(
	id_user        INT IDENTITY (1,1) NOT NULL ,
	nom_user       VARCHAR (50) NOT NULL ,
	prenom_user    VARCHAR (50) NOT NULL ,
	pseudo         VARCHAR (50) NOT NULL ,
	mail_user      VARCHAR (250) NOT NULL ,
	mdp_user       TEXT  NOT NULL ,
	actualise_le   DATETIME NOT NULL ,
	date_id      DATETIME NOT NULL  ,
	CONSTRAINT PK_User PRIMARY KEY (id_user)
);


/*------------------------------------------------------------
-- Table: Equipe
------------------------------------------------------------*/
CREATE TABLE Equipes(
	id_equipe    INT IDENTITY (1,1) NOT NULL ,
	nom_equipe   VARCHAR (50) NOT NULL ,
	user_id      INT   ,
	date_id  DATETIME   ,
	CONSTRAINT PK_Equipe PRIMARY KEY (id_equipe)
);


/*------------------------------------------------------------
-- Table: Matchs
------------------------------------------------------------*/
CREATE TABLE Matchs(
	id_match   INT IDENTITY (1,1) NOT NULL  ,
	date_id          DATETIME NOT NULL ,
	equipe_id          INT  NOT NULL ,
	equipe_oppose_id   INT  NOT NULL ,
	duree              DECIMAL  NOT NULL  ,
	CONSTRAINT PK_Matchs PRIMARY KEY (id_match)
);


/*------------------------------------------------------------
-- Table: Commentaire_match
------------------------------------------------------------*/
CREATE TABLE Commentaires_match(
	id_com_match     INT IDENTITY (1,1) NOT NULL ,
	text_com_match   TEXT  NOT NULL  ,
	CONSTRAINT PK_Commentaire_match PRIMARY KEY (id_com_match)
);


/*------------------------------------------------------------
-- Table: Statut
------------------------------------------------------------*/
CREATE TABLE Statuts(
	id_statut     INT IDENTITY (1,1) NOT NULL ,
	nom_statut    VARCHAR (50) NOT NULL ,
	desc_statut   TEXT  NOT NULL ,
	nb_tour       INT  NOT NULL  ,
	CONSTRAINT PK_Statut PRIMARY KEY (id_statut)
);


/*------------------------------------------------------------
-- Table: Type_obj
------------------------------------------------------------*/
CREATE TABLE Type_obj(
	id_type_obj     INT IDENTITY (1,1) NOT NULL ,
	nom_type_obj    VARCHAR (50) NOT NULL ,
	desc_type_obj   TEXT  NOT NULL  ,
	CONSTRAINT PK_Type_obj PRIMARY KEY (id_type_obj)
);


/*------------------------------------------------------------
-- Table: Nature
------------------------------------------------------------*/
CREATE TABLE Natures(
	id_nature     INT IDENTITY (1,1) NOT NULL ,
	nom_nature    VARCHAR (50) NOT NULL ,
	desc_nature   TEXT  NOT NULL  ,
	CONSTRAINT PK_Nature PRIMARY KEY (id_nature)
);


/*------------------------------------------------------------
-- Table: Meteo
------------------------------------------------------------*/
CREATE TABLE Meteos(
	id_meteo     INT IDENTITY (1,1) NOT NULL ,
	nom_meteo    VARCHAR (50) NOT NULL ,
	desc_meteo   TEXT  NOT NULL  ,
	CONSTRAINT PK_Meteo PRIMARY KEY (id_meteo)
);


/*------------------------------------------------------------
-- Table: Terrain
------------------------------------------------------------*/
CREATE TABLE Terrains(
	id_terrain     INT IDENTITY (1,1) NOT NULL ,
	nom_terrain    VARCHAR (50) NOT NULL ,
	desc_terrain   TEXT  NOT NULL  ,
	CONSTRAINT PK_Terrain PRIMARY KEY (id_terrain)
);


/*------------------------------------------------------------
-- Table: Talents
------------------------------------------------------------*/
CREATE TABLE Talents(
	id_talent    INT IDENTITY (1,1) NOT NULL ,
	nom_talent   VARCHAR (50) NOT NULL ,
	terrain_id   INT    ,
	CONSTRAINT PK_Talents PRIMARY KEY (id_talent)
);


/*------------------------------------------------------------
-- Table: Capacites
------------------------------------------------------------*/
CREATE TABLE Capacites(
	id_cap        INT  NOT NULL ,
	nom_cap       VARCHAR (50) NOT NULL ,
	proba_effet   DECIMAL (4,2)  NOT NULL ,
	type_id       INT  NOT NULL,
	meteo_id      INT   ,
	statut_id     INT   ,
	terrain_id    INT    ,
	cat_id   INT  NOT NULL ,
	CONSTRAINT PK_Capacites PRIMARY KEY (id_cap)
);


/*------------------------------------------------------------
-- Table: Generation
------------------------------------------------------------*/
CREATE TABLE Generations(
	id_gen    INT IDENTITY (1,1) NOT NULL ,
	nom_gen   VARCHAR (50) NOT NULL  ,
	CONSTRAINT PK_Generation PRIMARY KEY (id_gen)
);


/*------------------------------------------------------------
-- Table: Version
------------------------------------------------------------*/
CREATE TABLE Versions(
	id_version    INT IDENTITY (1,1) NOT NULL ,
	nom_version   VARCHAR (80) NOT NULL ,
	gen_id        INT    ,
	CONSTRAINT PK_Version PRIMARY KEY (id_version)
);


/*------------------------------------------------------------
-- Table: Objet
------------------------------------------------------------*/
CREATE TABLE Objets(
	id_objet      INT IDENTITY (1,1) NOT NULL ,
	nom_objet     VARCHAR (50) NOT NULL ,
	desc_objet    VARCHAR (50) NOT NULL ,
	version_id    INT   ,
	type_obj_id   INT    ,
	CONSTRAINT PK_Objet PRIMARY KEY (id_objet)
);


/*------------------------------------------------------------
-- Table: Stat
------------------------------------------------------------*/
CREATE TABLE Stats(
	id_stat    INT IDENTITY (1,1) NOT NULL ,
	nom_stat   VARCHAR (50) NOT NULL  ,
	CONSTRAINT PK_Stat PRIMARY KEY (id_stat)
);


/*------------------------------------------------------------
-- Table: type_avantage
------------------------------------------------------------*/
CREATE TABLE type_avantage(
	type_att_id         INT  NOT NULL ,
	type_cible_id   INT  NOT NULL ,
	coef_dom        INT  NOT NULL  ,
	CONSTRAINT PK_type_avantage PRIMARY KEY (type_att_id, type_cible_id)
);



/*------------------------------------------------------------
-- Table: Cap_version
------------------------------------------------------------*/
CREATE TABLE Cap_version(
	cap_id       INT  NOT NULL ,
	version_id   INT  NOT NULL ,
	pp           INT  NOT NULL ,
	puiss        INT  NOT NULL ,
	pre          INT  NOT NULL ,
	priorite     INT  NOT NULL ,
	base_crit    INT  NOT NULL  ,
	CONSTRAINT PK_Cap_version PRIMARY KEY (cap_id,version_id)
);



/*------------------------------------------------------------
-- Table: Tal_version
------------------------------------------------------------*/
CREATE TABLE Tal_version(
	version_id   INT  NOT NULL ,
	talent_id    INT  NOT NULL ,
	tal_desc     TEXT  NOT NULL  ,
	CONSTRAINT PK_Tal_version PRIMARY KEY (version_id,talent_id)
);


/*------------------------------------------------------------
-- Table: Pokemon
------------------------------------------------------------*/
CREATE TABLE Pokemons(
	id_pok            INT IDENTITY (1,1) NOT NULL ,
	nom_eng_pok       VARCHAR (80) NOT NULL ,
	nom_fra_pok       VARCHAR (80) NOT NULL ,
	num_pok           VARCHAR (6) NOT NULL ,
	taille_pok        DECIMAL (6,2)  NOT NULL ,
	poids_pok         DECIMAL (6,1)  NOT NULL ,
	base_experience   INT  NOT NULL ,
	base_hp           INT  NOT NULL ,
	base_att          INT  NOT NULL ,
	base_def          INT  NOT NULL ,
	base_sp_att       INT  NOT NULL ,
	base_sp_def       INT  NOT NULL ,
	base_vit          INT  NOT NULL ,
	legendaire        bit  NOT NULL ,
	shiny             bit  NOT NULL ,
	pok_img           VARCHAR (50) NOT NULL ,
	niveau            INT  NOT NULL ,
	evolution_id    INT   ,
	gen_id            INT    ,
	CONSTRAINT PK_Pokemon PRIMARY KEY (id_pok)
);


/*------------------------------------------------------------
-- Table: pokemon_type
------------------------------------------------------------*/
CREATE TABLE pokemon_type(
	pok_id       INT  NOT NULL ,
	type_id      INT  NOT NULL ,
	version_id   INT  NOT NULL ,
	emplac       TINYINT  NOT NULL  ,
	CONSTRAINT PK_pokemon_type PRIMARY KEY (pok_id,type_id,version_id)
);


/*------------------------------------------------------------
-- Table: Pok_talent
------------------------------------------------------------*/
CREATE TABLE pokemon_talent(
	pok_id      INT  NOT NULL ,
	talent_id   INT  NOT NULL  ,
	CONSTRAINT PK_Pok_talent PRIMARY KEY (pok_id,talent_id)
);


/*------------------------------------------------------------
-- Table: pokemon_capacite
------------------------------------------------------------*/
CREATE TABLE pokemon_capacite(
	cap_id   INT  NOT NULL ,
	pok_id   INT  NOT NULL ,
	niveau   SMALLINT  NOT NULL  ,
	CONSTRAINT PK_pokemon_capacite PRIMARY KEY (cap_id,pok_id)
);


/*------------------------------------------------------------
-- Table: stat_capture
------------------------------------------------------------*/
CREATE TABLE stat_capture(
	hab_id       INT  NOT NULL ,
	pok_id         INT  NOT NULL ,
	taux_capture   INT  NOT NULL ,
	taux_genre     INT  NOT NULL  ,
	CONSTRAINT PK_stat_capture PRIMARY KEY (hab_id ,pok_id)
);


/*------------------------------------------------------------
-- Table: equipier
------------------------------------------------------------*/
CREATE TABLE equipiers(
	equipe_id                     INT  NOT NULL ,
	talent_id                     INT  NOT NULL ,
	pok_id                        INT  NOT NULL ,
	cap1_id                        INT  NOT NULL ,
	cap2_id                      INT ,
	cap3_id                    INT ,
	cap4_id                     INT,
	objet_id                      INT ,
	nature_id                     INT  NOT NULL ,
	surnom                        VARCHAR (50) NOT NULL ,
	niveau                        INT  NOT NULL ,
	niv_bonheur                   INT  NOT NULL ,
	pv                            INT  NOT NULL ,
	ev                            INT  NOT NULL ,
	iv                            INT  NOT NULL ,
	att                           INT  NOT NULL ,
	def                           INT  NOT NULL ,
	att_spe                       INT  NOT NULL ,
	def_spe                       INT  NOT NULL ,
	vit                           INT  NOT NULL ,
	esquive                       INT  NOT NULL ,
	cap1_pp                       INT  NOT NULL ,
	cap1_puiss                    INT  NOT NULL ,
	cap1_pre                      INT  NOT NULL ,
	cap1_crit                     INT  NOT NULL ,
	cap2_pp                       INT  ,
	cap2_puiss                    INT  ,
	cap2_pre                      INT  ,
	cap2_crit                     INT ,
	cap3_pp                       INT  ,
	cap3_puiss                    INT ,
	cap3_pre                      INT,
	cap3_crit                     INT   ,
	cap4_pp                       INT   ,
	cap4_puiss                    INT   ,
	cap4_pre                      INT   ,
	cap4_crit                     INT   ,
	CONSTRAINT PK_equipier PRIMARY KEY (equipe_id,talent_id,pok_id,cap1_id,cap2_id,cap3_id,cap4_id,objet_id,nature_id)
);


/*------------------------------------------------------------
-- Table: action
------------------------------------------------------------*/
CREATE TABLE  actions(
	stat_id         INT  NOT NULL ,
	match_id        INT  NOT NULL ,
	talent_id       INT  NOT NULL ,
	objet_id        INT  NOT NULL ,
	meteo_id        INT  NOT NULL ,
	statut_id       INT  NOT NULL ,
	cap_id          INT  NOT NULL ,
	terrain_id      INT  NOT NULL ,
	type1_id         INT  NOT NULL ,
	type2_id   INT  NOT NULL ,
	pok_id          INT  NOT NULL ,
	baisse          bit  NOT NULL ,
	augmente        bit  NOT NULL ,
	val             DECIMAL  NOT NULL ,
	tour            INT  NOT NULL  ,
	CONSTRAINT PK_action PRIMARY KEY (stat_id,match_id,talent_id,objet_id,meteo_id,statut_id,cap_id,terrain_id,type1_id,type2_id,pok_id)
);

go


ALTER TABLE Users
	ADD CONSTRAINT FK_User_Date
	FOREIGN KEY (date_id)
	REFERENCES Dates(jjmmaaa);

ALTER TABLE Equipes
	ADD CONSTRAINT FK_Equipe_User
	FOREIGN KEY (user_id)
	REFERENCES Users(id_user);

ALTER TABLE Equipes
	ADD CONSTRAINT FK_Equipe_Date
	FOREIGN KEY (date_id)
	REFERENCES Dates(jjmmaaa);

ALTER TABLE Talents
	ADD CONSTRAINT FK_Talents_Terrain
	FOREIGN KEY (terrain_id)
	REFERENCES Terrains(id_terrain);

ALTER TABLE Capacites
	ADD CONSTRAINT FK_Capacites_Types
	FOREIGN KEY (type_id)
	REFERENCES Types(id_type);

ALTER TABLE Capacites
	ADD CONSTRAINT FK_Capacites_Meteo
	FOREIGN KEY (meteo_id)
	REFERENCES Meteos(id_meteo);

ALTER TABLE Capacites
	ADD CONSTRAINT FK_Capacites_Statut
	FOREIGN KEY (statut_id)
	REFERENCES Statuts(id_statut);

ALTER TABLE Capacites
	ADD CONSTRAINT FK_Capacites_Terrain
	FOREIGN KEY (terrain_id)
	REFERENCES Terrains(id_terrain);

ALTER TABLE Capacites
	ADD CONSTRAINT FK_Capacites_Categorie
	FOREIGN KEY (cat_id)
	REFERENCES Categories(id_cat);

ALTER TABLE Versions
	ADD CONSTRAINT FK_Version_Generation
	FOREIGN KEY (gen_id)
	REFERENCES Generations(id_gen);

ALTER TABLE Objets
	ADD CONSTRAINT FK_Objet_Version
	FOREIGN KEY (version_id)
	REFERENCES Versions(id_version);

ALTER TABLE Objets
	ADD CONSTRAINT FK_Objet_Type_obj
	FOREIGN KEY (type_obj_id)
	REFERENCES Type_obj(id_type_obj);

ALTER TABLE type_avantage
	ADD CONSTRAINT FK_type_avantage_att
	FOREIGN KEY (type_att_id)
	REFERENCES Types(id_type);

ALTER TABLE type_avantage
	ADD CONSTRAINT FK_type_avantage_cible
	FOREIGN KEY (type_cible_id)
	REFERENCES Types(id_type);

ALTER TABLE Cap_version
	ADD CONSTRAINT FK_Cap_version_Capacites
	FOREIGN KEY (cap_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE Cap_version
	ADD CONSTRAINT FK_Cap_version_Version
	FOREIGN KEY (version_id)
	REFERENCES Versions(id_version);

ALTER TABLE Matchs
	ADD CONSTRAINT FK_Matchs_Date
	FOREIGN KEY (date_id)
	REFERENCES Dates(jjmmaaa);

ALTER TABLE Matchs
	ADD CONSTRAINT FK_Matchs_Equipe
	FOREIGN KEY (equipe_id)
	REFERENCES Equipes(id_equipe);

ALTER TABLE Matchs
	ADD CONSTRAINT FK_Equipe_Matchs
	FOREIGN KEY (equipe_oppose_id)
	REFERENCES Equipes(id_equipe);

ALTER TABLE Tal_version
	ADD CONSTRAINT FK_Tal_version_Version
	FOREIGN KEY (version_id)
	REFERENCES Versions(id_version);

ALTER TABLE Tal_version
	ADD CONSTRAINT FK_Tal_version_Talents
	FOREIGN KEY (talent_id)
	REFERENCES Talents(id_talent);

ALTER TABLE Pokemons
	ADD CONSTRAINT FK_Pokemon_Pokemon
	FOREIGN KEY (evolution_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE Pokemons
	ADD CONSTRAINT FK_Pokemon_Generation
	FOREIGN KEY (gen_id)
	REFERENCES Generations(id_gen);

ALTER TABLE pokemon_type
	ADD CONSTRAINT FK_pokemon_type
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE pokemon_type
	ADD CONSTRAINT FK_pokemon_type1
	FOREIGN KEY (type_id)
	REFERENCES Types(id_type);

ALTER TABLE pokemon_type
	ADD CONSTRAINT FK_pokemon_type2
	FOREIGN KEY (version_id)
	REFERENCES Versions(id_version);

ALTER TABLE pokemon_talent
	ADD CONSTRAINT FK_Pok_talent_Pokemon
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE pokemon_talent
	ADD CONSTRAINT FK_Pok_talent_Talents
	FOREIGN KEY (talent_id)
	REFERENCES Talents(id_talent);

ALTER TABLE pokemon_capacite
	ADD CONSTRAINT FK_pokemon_capacite
	FOREIGN KEY (cap_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE pokemon_capacite
	ADD CONSTRAINT FK_capacite_pokemon
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE stat_capture
	ADD CONSTRAINT FK_stat_capture_Habitat
	FOREIGN KEY (hab_id)
	REFERENCES Habitats(id_hab);

ALTER TABLE stat_capture
	ADD CONSTRAINT FK_stat_capture_Pokemon
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Equipe
	FOREIGN KEY (equipe_id)
	REFERENCES Equipes(id_equipe);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Talents
	FOREIGN KEY (talent_id)
	REFERENCES Talents(id_talent);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Pokemon
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Cap1
	FOREIGN KEY (cap1_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Cap2
	FOREIGN KEY (cap2_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Cap3
	FOREIGN KEY (cap3_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Cap4
	FOREIGN KEY (cap4_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Objet
	FOREIGN KEY (objet_id)
	REFERENCES Objets(id_objet);

ALTER TABLE equipiers
	ADD CONSTRAINT FK_equipier_Nature
	FOREIGN KEY (nature_id)
	REFERENCES Natures(id_nature);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Stat
	FOREIGN KEY (stat_id)
	REFERENCES Stats(id_stat);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Matchs
	FOREIGN KEY (match_id)
	REFERENCES Matchs(id_match);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Talents
	FOREIGN KEY (talent_id)
	REFERENCES Talents(id_talent);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Objet
	FOREIGN KEY (objet_id)
	REFERENCES Objets(id_objet);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Meteo
	FOREIGN KEY (meteo_id)
	REFERENCES Meteos(id_meteo);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Statut
	FOREIGN KEY (statut_id)
	REFERENCES Statuts(id_statut);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Capacites
	FOREIGN KEY (cap_id)
	REFERENCES Capacites(id_cap);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Terrain
	FOREIGN KEY (terrain_id)
	REFERENCES Terrains(id_terrain);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Type1
	FOREIGN KEY (type1_id)
	REFERENCES Types(id_type);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Type2
	FOREIGN KEY (type2_id)
	REFERENCES Types(id_type);

ALTER TABLE  actions
	ADD CONSTRAINT FK_action_Pokemon
	FOREIGN KEY (pok_id)
	REFERENCES Pokemons(id_pok);
