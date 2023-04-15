import { MigrationInterface, QueryRunner, Table } from "typeorm"

export class CreateContractsTable1681567569268 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.createTable(
            new Table({
                name: "Contracts",
                columns: [
                    {
                        name: "id",
                        type: "varchar",
                        length: "100",
                        isPrimary: true
                    },
                    {
                        name: "name",
                        type: "varchar",
                        length: "100",
                        isUnique: true,
                        isNullable: false
                    },
                    {
                        name: "addressContract",
                        type: "varchar",
                        length: "100",
                        isNullable: false
                    },
                    {
                        name: "abi",
                        type: "text"
                    },
                    {
                        name: "bytecode",
                        type: "text"
                    },
                    {
                        name: "createdAt",
                        type: "datetime"
                    }
                ] 
            })
        )
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.dropTable("Contracts");
    }

}
