import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Home from './pages/Home';
import Login from './pages/Login';
import Register from './pages/Register';

function App() {
  const [userData, setUserData] = useState(null);

  return (
    <Router>
      <Routes>
        {/* Redirect based on authentication state */}
        <Route 
          path="/" 
          element={userData ? <Navigate to="/home" /> : <Navigate to="/login" />} 
        />
        
        <Route 
          path="/login" 
          element={<Login onLoginSuccess={(data) => setUserData(data)} />} 
        />
        
        <Route 
          path="/register" 
          element={<Register />} 
        />
        
        {/* Home route for authenticated users */}
        <Route 
          path="/home" 
          element={userData ? <Home customerId={userData.customerId} /> : <Navigate to="/login" />} 
        />
      </Routes>
    </Router>
  );
}

export default App;
