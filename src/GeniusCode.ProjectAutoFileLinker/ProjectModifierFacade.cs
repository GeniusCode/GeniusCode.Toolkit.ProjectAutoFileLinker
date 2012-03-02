using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public class ProjectModifierFacade : IProjectModifierFacade
    {
        private readonly IProjectFileLinker _linker;
        private readonly IRelativeFileMapper _fileMapper;
        private readonly Project _project;

        public Project Project
        {
            get { return _project; }
        }

        public ProjectModifierFacade(IProjectFileLinker linker, IRelativeFileMapper fileMapper, Project project)
        {
            _linker = linker;
            _fileMapper = fileMapper;
            _project = project;
        }

        public void EstablishLinkToFile(EstablishLinkParams linkParams)
        {
            var includeValue = _fileMapper.ConvertPathToRelative(_project.FullPath, linkParams.FilenameToLink);
            _linker.LinkToFile(_project,linkParams.BuildAction,includeValue,linkParams.ProjectTargetPath);
        }

        public ProjectModifierFacade(Project project): this(new ProjectFileLinker(),new RelativeFileMapper(), project)
        {
        }
    }
}