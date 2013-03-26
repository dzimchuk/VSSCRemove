# VSSCRemove

VSSCRemove is a tool that removes source control dependencies from Visual Studio project and solution files. See http://www.dzimchuk.net/blog/post/Removing-source-control-dependencies-from-Visual-Studio-project-files.aspx for the reasoning behind it.

Just output your stuff (it can be a directory tree of projects and solutions) somewhere, remove read-only attributes recursively and run the tool passing it a root path where you’d put your stuff. The tool should be able to remove all SCC specific things but I might have missed something. Well, you have the tool, you can add what I’ve missed ;)

## License

Licensed under the Microsoft Public License (MS-PL)
