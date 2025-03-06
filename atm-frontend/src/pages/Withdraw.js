import React, { useState } from 'react';
import axios from 'axios';

const Withdraw = () => {
    const [customerId, setCustomerId] = useState('');
    const [amount, setAmount] = useState('');
    const [message, setMessage] = useState('');

    const handleWithdraw = async () => {
        try {
            const response = await axios.post('http://localhost:5014/api/atm/withdraw', { customerId, amount });
            setMessage(response.data.Message + ' New Balance: ' + response.data.Balance);
        } catch (error) {
            alert('Error: ' + error.response.data);
        }
    };

    return (
        <div>
            <h2>Withdraw</h2>
            <input
                type="number"
                placeholder="Enter Customer ID"
                value={customerId}
                onChange={(e) => setCustomerId(e.target.value)}
            />
            <input
                type="number"
                placeholder="Enter Amount"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
            />
            <button onClick={handleWithdraw}>Withdraw</button>
            <p>{message}</p>
        </div>
    );
};

export default Withdraw;
