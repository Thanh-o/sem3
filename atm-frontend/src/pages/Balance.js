import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { toast } from 'react-toastify';
import { useUser } from './UserContext';

const Balance = () => {
    const { user } = useUser();
    const customerId = user?.customerId;
    const [balance, setBalance] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        console.log('Customer ID:', customerId);
        if (customerId) {
            getBalance(customerId);
        } else {
            toast.error('Customer ID not found. Please log in.');
            setLoading(false);
        }
    }, [customerId]);

    const getBalance = async (customerId) => {
        try {
            const response = await axios.get(`http://localhost:5014/api/atm/Balance/${customerId}`);
            console.log('Response data:', response.data); // Kiểm tra cấu trúc dữ liệu
            setBalance(response.data.Balance);
        } catch (error) {
            toast.error('Error fetching balance: ' + (error.response ? error.response.data : error.message));
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h2>Your Balance</h2>
            {loading ? (
                <p>Loading balance...</p>
            ) : (
                balance !== null ? (
                    <p>Balance: ${balance}</p>
                ) : (
                    <p>No balance available.</p>
                )
            )}
        </div>
    );
};

export default Balance;
