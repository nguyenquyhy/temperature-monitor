/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: false
                }
            }
        },
        typescript: {
            base: {
                src: ['Scripts/app/*.ts', 'Scripts/app/**/*.ts'],
                dest: 'wwwroot/js',
                options: {
                    module: 'amd', //or commonjs 
                    target: 'es5', //or es3 
                    basePath: 'Scripts/app',
                    sourceMap: false,
                    declaration: false,
                    removeComments: true,
                    watch: true
                }
            }
        },
        tsd: {
            refresh: {
                options: {
                    // execute a command 
                    command: 'reinstall',

                    //optional: always get from HEAD 
                    latest: true,

                    // optional: specify config file 
                    config: 'tsd.json',

                    // experimental: options to pass to tsd.API 
                    opts: {
                        // props from tsd.Options 
                    }
                }
            }
        }

    });

    grunt.registerTask("default", ["bower:install"]);

    grunt.loadNpmTasks('grunt-bower-task');
    grunt.loadNpmTasks('grunt-typescript');
    grunt.loadNpmTasks('grunt-tsd');
};