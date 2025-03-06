import React, { useState } from 'react';
import axios from 'axios';

const TransactionHistory = () => {
    const [customerId, setCustomerId] = useState('');
    const [transactions, setTransactions] = useState([]);

    const getTransactionHistory = async () => {
        try {
            const response = await axios.get(`http://localhost:5014/api/atm/transaction-history/${customerId}`);
            setTransactions(response.data);
        } catch (error) {
            alert('Error fetching transactions: ' + error.response.data);
        }
    };

    return (
        <div>
            <h2>Transaction History</h2>
            <input
                type="number"
                placeholder="Enter Customer ID"
                value={customerId}
                onChange={(e) => setCustomerId(e.target.value)}
            />
            <button onClick={getTransactionHistory}>Get History</button>
            <ul>
                {transactions.map((transaction, index) => (
                    <li key={index}>
                        {transaction.Timestamp}: {transaction.Description} - ${transaction.Amount}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default TransactionHistory;
