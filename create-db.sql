CREATE TABLE "Surveys"
(
    "Id"       SERIAL PRIMARY KEY,
    "PublicId" UUID NOT NULL,
    "Title"    TEXT NOT NULL
);

CREATE TABLE "Interviews"
(
    "Id"          SERIAL PRIMARY KEY,
    "PublicId"    UUID        NOT NULL,
    "SurveyId"    INTEGER     NOT NULL,
    "StartedAt"   TIMESTAMPTZ NOT NULL,
    "CurrentStep" INTEGER     NOT NULL,
    CONSTRAINT "FK_Interviews_Surveys" FOREIGN KEY ("SurveyId")
        REFERENCES "Surveys" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Questions"
(
    "Id"       SERIAL PRIMARY KEY,
    "PublicId" UUID    NOT NULL,
    "SurveyId" INTEGER NOT NULL,
    "OrderId"  INTEGER NOT NULL,
    "Text"     TEXT    NOT NULL,
    CONSTRAINT "FK_Questions_Surveys" FOREIGN KEY ("SurveyId")
        REFERENCES "Surveys" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Answers"
(
    "Id"         SERIAL PRIMARY KEY,
    "PublicId"   UUID    NOT NULL,
    "QuestionId" INTEGER NOT NULL,
    "Value"      TEXT    NOT NULL,
    CONSTRAINT "FK_Answers_Questions" FOREIGN KEY ("QuestionId")
        REFERENCES "Questions" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Results"
(
    "Id"          SERIAL PRIMARY KEY,
    "InterviewId" INTEGER NOT NULL,
    "QuestionId"  INTEGER NOT NULL,
    "AnswerId"    INTEGER NOT NULL,
    CONSTRAINT "FK_Results_Interviews" FOREIGN KEY ("InterviewId")
        REFERENCES "Interviews" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Results_Questions" FOREIGN KEY ("QuestionId")
        REFERENCES "Questions" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Results_Answers" FOREIGN KEY ("AnswerId")
        REFERENCES "Answers" ("Id") ON DELETE CASCADE
);

INSERT INTO "Surveys" ("Id", "PublicId", "Title")
VALUES (1, '229bd81e-7ab5-45f1-9b3c-31952c9c073f', 'Test Survey');

INSERT INTO "Questions" ("Id", "OrderId", "PublicId", "SurveyId", "Text")
VALUES (1, 0, 'ef828b47-78f6-45b4-83f4-be6f13a0a2d1', 1, 'Какой тип отдыха вам больше нравится?'),
       (2, 1, '67ff70eb-c647-44f8-a93e-2109972e8f12', 1, 'Какой напиток вы предпочитаете по утрам?'),
       (3, 3, '2261aa79-9aec-4e54-839a-5433304cec8b', 1, 'Какие жанры фильмов вам нравятся?');

INSERT INTO "Answers" ("Id", "PublicId", "QuestionId", "Value")
VALUES (1, 'c59f8fba-9d6b-44b5-9dac-4ea9f86283d0', 1, 'Пляжный отдых'),
       (2, '7a2a4cc2-5a40-4fa7-aaa9-b612674e97e3', 1, 'Походы и природа'),
       (3, '11a6cc81-427c-4530-aec2-e406d274fde4', 1, 'Городские экскурсии'),
       (4, '75f33647-c40f-48a3-99b9-de8dd7229d2c', 2, 'Кофе'),
       (5, '02900505-eee5-49cf-a0c1-56706910c3c0', 2, 'Чай'),
       (6, '054d7785-d3c4-4e40-b4a5-248e4439e4d9', 2, 'Сок'),
       (7, '821ea7fd-857b-4483-ac0c-ffcc34c86729', 2, 'Вода'),
       (8, '27155983-12f6-43be-993c-df5c00e8ff65', 3, 'Ужасы'),
       (9, 'f0685154-26a7-4652-8a5c-4917e1c7740d', 3, 'Комедия'),
       (10, '6590cff6-6cea-4698-8513-62bb9c28acad', 3, 'Фантастика'),
       (11, '78f5ccfe-46c9-488b-94c2-0a1378d1d793', 3, 'Детектив');

CREATE INDEX "IX_Answers_QuestionId" ON "Answers" ("QuestionId");
CREATE UNIQUE INDEX "IX_Interviews_PublicId" ON "Interviews" ("PublicId");
CREATE INDEX "IX_Interviews_SurveyId" ON "Interviews" ("SurveyId");
CREATE UNIQUE INDEX "IX_Questions_PublicId" ON "Questions" ("PublicId");
CREATE INDEX "IX_Questions_SurveyId_OrderId" ON "Questions" ("SurveyId", "OrderId");
