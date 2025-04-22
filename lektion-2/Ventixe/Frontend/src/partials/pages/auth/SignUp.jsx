import React from 'react'
import { useAuth } from '../../../contexts/AuthContext'

const SignUp = () => {
  try {
    const { signUp } = useAuth()
  }
  catch { }
  

  return (
    <div>SignUp</div>
  )
}

export default SignUp