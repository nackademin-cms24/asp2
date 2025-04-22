import { lazy } from 'react'
import { Navigate } from "react-router-dom";

import AuthLayout from '../partials/layouts/AuthLayout'
import PortalLayout from '../partials/layouts/PortalLayout'
import Unauthorized from "../partials/pages/auth/Unauthorized";

const NotFound = lazy(() => import('../partials/pages/NotFound'))

const SignUp = lazy(() => import('../partials/pages/auth/SignUp'))
const SignIn = lazy(() => import('../partials/pages/auth/SignIn'))

const UserDashboard = lazy(() => import('../partials/pages/user/Dashboard'))
const UserBookings = lazy(() => import('../partials/pages/user/Bookings'))

const AdminDashboard = lazy(() => import('../partials/pages/admin/Dashboard'))
const AdminBookings = lazy(() => import('../partials/pages/admin/Bookings'))

export const routes = [
    {
        children: [
            { path: '/',element: <Navigate to="/dashboard" replace /> }
        ]
    },
    {
        layout: AuthLayout,
        children: [
            { path: '/signup', element: <SignUp /> },
            { path: '/login', element: <SignIn /> },
            { path: '/denied', element: <Unauthorized /> },
        ]
    },
    {
        layout: PortalLayout,
        protected: true,
        children: [
            { path: '/dashboard', element: <UserDashboard /> },
            { path: '/bookings', element: <UserBookings /> },
        ]
    },
    {
        layout: PortalLayout,
        protected: true,
        adminOnly: true,
        children: [
            { path: '/admin/dashboard', element: <AdminDashboard /> },
            { path: '/admin/bookings', element: <AdminBookings /> },
        ]
    },
    {
        children: [
            { path: '*', element: <NotFound /> }
        ]
    }
]