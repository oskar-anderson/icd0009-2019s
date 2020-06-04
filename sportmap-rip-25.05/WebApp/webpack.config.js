const path = require('path');
const {CleanWebpackPlugin} = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require('copy-webpack-plugin');

module.exports = ({production} = {}, {} = {}) => ({
    entry: {
        site: './wwwroot/js/site.ts',
        'jquery.validate.globalize': './wwwroot/js/jquery.validate.globalize.js'
    },
    devtool: production ? 'nosources-source-map' : 'cheap-module-eval-source-map',
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            },
            {test: /\.(png|gif|jpg|cur)$/i, loader: 'url-loader', options: {limit: 8192}},
            {
                test: /\.woff2(\?v=[0-9]\.[0-9]\.[0-9])?$/i,
                loader: 'url-loader',
                options: {limit: 10000, mimetype: 'application/font-woff2'}
            },
            {
                test: /\.woff(\?v=[0-9]\.[0-9]\.[0-9])?$/i,
                loader: 'url-loader',
                options: {limit: 10000, mimetype: 'application/font-woff'}
            },
            // load these fonts normally, as files:
            {test: /\.(ttf|eot|svg|otf)(\?v=[0-9]\.[0-9]\.[0-9])?$/i, loader: 'file-loader'},
        ],
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
        alias: {
            'cldr$': 'cldrjs',
            'cldr': 'cldrjs/dist/cldr'
        }
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'wwwroot/dist'),
    },
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin(),
        new CopyPlugin(
            [
                {from: 'node_modules/cldr-core/supplemental/likelySubtags.json', to: 'cldr-core/supplemental'},
                {from: 'node_modules/cldr-core/supplemental/numberingSystems.json', to: 'cldr-core/supplemental'},
                {from: 'node_modules/cldr-core/supplemental/timeData.json', to: 'cldr-core/supplemental'},
                {from: 'node_modules/cldr-core/supplemental/weekData.json', to: 'cldr-core/supplemental'},

                {from: 'node_modules/cldr-numbers-modern/main/et/', to: 'cldr-numbers-modern/main/et/'},
                {from: 'node_modules/cldr-dates-modern/main/et/', to: 'cldr-dates-modern/main/et/'},

                {from: 'node_modules/cldr-numbers-modern/main/en-GB/', to: 'cldr-numbers-modern/main/en/'},
                {from: 'node_modules/cldr-dates-modern/main/en-GB/', to: 'cldr-dates-modern/main/en/'},

                {from: 'node_modules/cldr-numbers-modern/main/ru/', to: 'cldr-numbers-modern/main/ru/'},
                {from: 'node_modules/cldr-dates-modern/main/ru/', to: 'cldr-dates-modern/main/ru/'},
            ]
        ),
    ],
});
