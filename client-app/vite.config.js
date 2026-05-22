import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Все запросы, начинающиеся с /api, перенаправляются на бэкенд
      '/api': 'http://10.211.55.4:5283'
    }
  }
})