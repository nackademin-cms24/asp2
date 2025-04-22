import React from 'react'
import { Outlet } from 'react-router-dom'

const PortalLayout = () => {
  return (
    <div className="portal-layout">
      <Outlet />
    </div>
  )
}

export default PortalLayout