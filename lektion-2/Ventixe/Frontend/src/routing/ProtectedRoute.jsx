import React from 'react'
import { Navigate } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'

const ProtectedRoute = ({children}) => {
    try {
        const { isAuthenticated } = useAuth()
     
        if (isAuthenticated && isAuthenticated !== undefined) {
            return children
        }
    
    }
    catch { }

    return <Navigate to="/login" replace />
}

export default ProtectedRoute