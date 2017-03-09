var path = require("path"),
    webpack = require("webpack");

module.exports = {
    entry: path.join(__dirname, "wwwroot/js/site.js"),
    output: {
        path: path.join(__dirname, "wwwroot/assets"),
        publicPath: "assets/",
        filename: "js/bundle.js"
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jquery: "jQuery",
            "windows.jQuery": "jquery"
        })
    ],
    module: {
        loaders: [
            { test: /\.scss/, loader: "style!css!sass?includePaths[]=" + path.resolve(__dirname, "./node_modules") },
            { test: /\.woff(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "url-loader?limit=10000&minetype=application/font-woff&name=fonts/[hash].[ext]" },
            { test: /\.woff2(\?v=\d+\.\d+\.\d+)?$/, loader: "url?limit=10000&minetype=application/font-woff&name=fonts/[hash].[ext]" },
            { test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/, loader: "file-loader?name=fonts/[hash].[ext]" },
            {
                test: /\.(jpe?g|png|gif|svg)$/i, loaders: [
                    "file?hash=sha512&digest=hex&name=images/[hash].[ext]",
                    "image-webpack?bypassOnDebug&optimizationLevel=7&interlaced=false"
                ]
            }
        ]
    },
    devServer: {
        host: "0.0.0.0"
    },
    watchOptions: {
        poll: true
    }
}