using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "application_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "attribute_types",
                columns: table => new
                {
                    attribute_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_types", x => x.attribute_type_id);
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    attribute_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.attribute_id);
                });

            migrationBuilder.CreateTable(
                name: "cart_statuses",
                columns: table => new
                {
                    cart_status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_statuses", x => x.cart_status_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    parent_category_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalTable: "categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_application_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "application_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_application_users_UserId",
                        column: x => x.UserId,
                        principalTable: "application_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_application_users_UserId",
                        column: x => x.UserId,
                        principalTable: "application_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_application_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "application_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_application_users_UserId",
                        column: x => x.UserId,
                        principalTable: "application_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_application_users_UserId",
                        column: x => x.UserId,
                        principalTable: "application_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification_channels",
                columns: table => new
                {
                    notification_channel_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ApplicationUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_channels", x => x.notification_channel_id);
                    table.ForeignKey(
                        name: "FK_notification_channels_application_users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "application_users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    closed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK_carts_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "application_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carts_cart_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "cart_statuses",
                        principalColumn: "cart_status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    stock_quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_product",
                columns: table => new
                {
                    cart_product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    cart_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_product", x => x.cart_product_id);
                    table.ForeignKey(
                        name: "FK_cart_product_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "cart_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_product_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_images",
                columns: table => new
                {
                    product_image_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    content = table.Column<byte[]>(type: "bytea", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    product_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_images", x => x.product_image_id);
                    table.ForeignKey(
                        name: "FK_product_images_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "application_roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "user", "USER" },
                    { 2, null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "attribute_types",
                columns: new[] { "attribute_type_id", "name" },
                values: new object[,]
                {
                    { 1, "Используется для поиска" },
                    { 2, "Не используется для поиска" }
                });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "attribute_id", "name" },
                values: new object[,]
                {
                    { 1, "Цвет" },
                    { 2, "Размер" }
                });

            migrationBuilder.InsertData(
                table: "cart_statuses",
                columns: new[] { "cart_status_id", "name" },
                values: new object[,]
                {
                    { 1, "Новая" },
                    { 2, "Оформлена" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "category_id", "name", "parent_category_id" },
                values: new object[] { 1, "Музыкальные инструменты", null });

            migrationBuilder.InsertData(
                table: "notification_channels",
                columns: new[] { "notification_channel_id", "ApplicationUserId", "name" },
                values: new object[,]
                {
                    { 1, null, "Email" },
                    { 2, null, "Telegram" },
                    { 3, null, "Личный кабинет" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "category_id", "name", "parent_category_id" },
                values: new object[,]
                {
                    { 2, "Гитары", 1 },
                    { 3, "Клавишные", 1 },
                    { 4, "Ударные", 1 },
                    { 5, "Бас гитары", 2 },
                    { 6, "Электрогитары", 2 }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_id", "category_id", "created_at", "description", "image_url", "name", "price", "stock_quantity", "updated_at" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2024, 11, 14, 16, 41, 28, 169, DateTimeKind.Utc).AddTicks(2141), "ROCKDALE Stars PB Bass – универсальная бас-гитара с формой корпуса пресижн бас (precision bass). Звукосниматель типа сплит-сингл (split-single) позволяет гитаре звучать особенно напористо, дает характерный мощный звук с особенно выраженными средними частотами. Корпус гитары изготовлен из тополя, гриф из клена с профилем C-Shape. Накладка грифа из HPL-композита - современного материала, устойчивого к резким изменениям температуры и влажности. В грифе установлен анкер для регулировки высоты струн. Струны 45-105 из никелированной стали. В комплект входит набор ключей для отстройки гитары, кабель для подключения Jack-Jack и инструкция по уходу за иструментом. Мензура 864мм.", "https://avatars.mds.yandex.net/get-mpic/1937077/img_id1984092563303990645.jpeg/optimize", "ROCKDALE Stars Precision Bass", 13599m, 134, null },
                    { 2, 6, new DateTime(2024, 11, 14, 16, 41, 28, 169, DateTimeKind.Utc).AddTicks(2147), "ROCKDALE Stars HT HSS – универсальная электрогитара, полностью выполненная в стильном черном цвете. Подходит для обучения. Форма корпуса стратокастер (stratocaster), керамические звукосниматели HSS, 5-ти позиционный переключатель, 2 ручки тона(tone), ручка громкости(volume) и фиксированный бридж (hardtail bridge) дают возможность исполнять любой стиль музыки. Корпус из тополя, гриф из клена с профилем C-Shape. Накладка из HPL- композита - современного материала, устойчивого к резким изменениям температуры и влажности. В грифе установлен анкер для регулировки высоты струн на грифом. Струны 10-46 из никелированной стали. В комплект входят ключи для отстройки гитары, кабель, для подключения Jack-Jack и инструкция по уходу за инструментом.", "https://avatars.mds.yandex.net/get-mpic/1522540/img_id8828176765638498087.jpeg/optimize", "ROCKDALE Stars HT HSS Black Limited Edition", 11899m, 98, null }
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "application_roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "application_users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "application_users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_cart_product_cart_id",
                table: "cart_product",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_product_product_id",
                table: "cart_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_status_id",
                table: "carts",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_user_id",
                table: "carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_notification_channels_ApplicationUserId",
                table: "notification_channels",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_product_images_product_id",
                table: "product_images",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "attribute_types");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "cart_product");

            migrationBuilder.DropTable(
                name: "notification_channels");

            migrationBuilder.DropTable(
                name: "product_images");

            migrationBuilder.DropTable(
                name: "application_roles");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "application_users");

            migrationBuilder.DropTable(
                name: "cart_statuses");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
