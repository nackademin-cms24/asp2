import React from 'react'
import { Route, Routes } from 'react-router-dom'
import { routes } from './routes.config'

import AdminRoute from './AdminRoute'
import ProtectedRoute from './ProtectedRoute'

const RouteRenderer = () => {
    return (

        <Routes>
            {routes.map((config, index) => {
                const LayoutComponent = config.layout
                const Wrapper = config.adminOnly
                    ? AdminRoute
                    : config.protected
                        ? ProtectedRoute
                        : ({children}) => <>{children}</>

                if (!LayoutComponent) {
                    return config.children.map(({ path, element }) => <Route key={path} path={path} element={element} />)
                }

                return (
                    <Route key={index} element={<Wrapper><LayoutComponent /></Wrapper>}>
                        { 
                            config.children.map(({ path, element}) => (
                                <Route key={path} path={path} element={element} />
                            ))
                        }
                    </Route>
                )
            })}
        </Routes>
    )
}

export default RouteRenderer