import React from 'react'
import { Navigate } from 'react-router-dom'
import { useAuth } from '../contexts/AuthContext'

const AdminRoute = ({children}) => {
    try {
        const { isAuthenticated, isAdmin } = useAuth()
    
        if (isAuthenticated && isAuthenticated !== undefined) {
            if (isAdmin) {
                return children 
            }
    
            return <Navigate to="/denied" replace />
        }
    
    }
    catch { }
   
    return <Navigate to="/login" replace />
}

export default AdminRoute