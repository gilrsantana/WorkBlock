import { contractModel } from "../../models/contractModel";
import { myConnection } from "./connection";

export function insertContract(data: contractModel) {
	const connection = myConnection();
	let ref = -1;
	connection.connect((error) => {
		if (error) {
			console.error('Error connecting to MySQL database: ', error);
		} else {
			console.log('Connected to MySQL database');
			let query = "SELECT id FROM CONTRACTS WHERE name = '" + data.name + "';"
			connection.query(query, data, (error, result) => {
				if (error) {
					console.error('Error executing query: ', error);
				} else {
					const insertData = data;
					if (result.length < 1) {
						query = 'INSERT INTO CONTRACTS SET ?';
						connection.query(query, insertData, (error) => {
							if (error) {
								console.error('Error executing insert query:', error);
							} else {
								console.log('Data inserted successfully');
							}
						})
						connection.end();
					} else {
						ref = result[0].id;
						console.log(ref);
						const updateData = data;
						query = "UPDATE CONTRACTS SET name = '" + updateData.name + "', " +
							"addressContract = '" + updateData.addressContract + "', " +
							"abi = '" + updateData.abi + "', " +
							"bytecode = '" + updateData.bytecode + "' " +
							"WHERE id = '" + ref + "';";
						connection.query(query, updateData, (error) => {
							if (error) {
								console.error('Error executing update query:', error);
							} else {
								console.log('Data updated successfully');
							}
						})
						connection.end();
					}
				}
			})
		}
	})
}