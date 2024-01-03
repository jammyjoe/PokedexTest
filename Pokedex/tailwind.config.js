/** @type {import('tailwindcss').Config} */

const { join } = require('path');
module.exports = {
    content: [
        "./Pages/**/*.{html,cshtml}", //Add these two
        "./wwwroot/**/*.{html,htm}"
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}

