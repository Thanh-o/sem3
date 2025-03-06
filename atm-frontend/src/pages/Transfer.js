import React, { useState } from 'react';
import axios from 'axios';

const Transfer = () => {
    const [senderId, setSenderId] = useState('');
    const [receiverId, setReceiverId] = useState('');
    const [amount, setAmount] = useState('');
    const [message, setMessage] = useState('');

    const handleTransfer = async () => {
        try {
            const response = await axios.post('http://localhost:5014/api/atm/transfer', { senderId, receiverId, amount });
            setMessage(response.data.Message);
        } catch (error) {
            alert('Error: ' + error.response.data);
        }
    };

    return (
        <div>
            <h2>Transfer</h2>
            <input
                type="number"
                placeholder="Sender ID"
                value={senderId}
                onChange={(e) => setSenderId(e.target.value)}
            />
            <input
                type="number"
                placeholder="Receiver ID"
                value={receiverId}
                onChange={(e) => setReceiverId(e.target.value)}
            />
            <input
                type="number"
                placeholder="Amount"
                value={amount}
                onChange={(e) => setAmount(e.target.value)}
            />
            <button onClick={handleTransfer}>Transfer</button>
            <p>{message}</p>
        </div>
    );
};

export default Transfer;
