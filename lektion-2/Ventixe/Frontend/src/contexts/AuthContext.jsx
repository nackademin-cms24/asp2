import { createContext, useContext, useState } from "react"

const AuthContext = createContext()

export const AuthProvider = ({children}) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false)
    const [isAdmin, setIsAdmin] = useState(false)
    const [user, setUser] = useState(null)
    
    const signIn = async ({email, password, isPersistent}) => {

    }

    const signUp = async ({email}) => {

    }

    return (
        <AuthContext.Provider value={{isAuthenticated, isAdmin, user, signUp, signIn}}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext)
    return context
}